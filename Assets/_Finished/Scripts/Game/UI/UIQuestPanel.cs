using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ISU.Example
{
    public class UIQuestPanel : MonoBehaviour
    {
        [SerializeField] QuestManagerVariable m_QuestManger;
        [SerializeField] GameObject m_Panel;
        [SerializeField] RectTransform m_MissionBoardContainer;
        [SerializeField] VerticalLayoutGroup m_ContainerLayout;
        [SerializeField] ContentSizeFitter m_ContainerSizeFittter;
        [SerializeField] UIQuestBoard m_MissionBoardPrefab;

        IEnumerator CurrentBuildPanelCoroutine;
        private void Start()
        {

            m_Panel.SetActive(false);
        }

        private void OnEnable()
        {
            QuestHolder.m_OnQuestPanelOpen += PanelOpen;
        }
        private void OnDisable()
        {
            QuestHolder.m_OnQuestPanelOpen -= PanelOpen;
        }
        void PanelOpen(Quest[] quests)
        {
            CurrentBuildPanelCoroutine = BuildPanelContent(quests);
            StartCoroutine(CurrentBuildPanelCoroutine);
        }

        IEnumerator BuildPanelContent(Quest[] quests)
        {
            foreach (var existBoard in m_MissionBoardContainer.GetComponentsInChildren<UIQuestBoard>())
            {
                Destroy(existBoard.gameObject);
            }

            AutoLayout(true);
            yield return null;

            GameManager.FPSCursorLock(false);
            m_Panel.SetActive(true);

            float fadeDuration = 0.1f;
            WaitForSeconds buildDelay = new WaitForSeconds(fadeDuration);

            for (int i = 0; i < quests.Length; i++)
            {

                Quest curQuest = quests[i];
                var board = Instantiate(m_MissionBoardPrefab, m_MissionBoardContainer);
                board.SetInfos(curQuest, () => Accept(curQuest, board), fadeDuration);
                yield return buildDelay;
            }
            yield return null;
            AutoLayout(false);
        }

        private void AutoLayout(bool on)
        {
            m_ContainerSizeFittter.enabled = on;
            m_ContainerLayout.enabled = on;
        }

        public void Exit()
        {
            if (CurrentBuildPanelCoroutine != null)
                StopCoroutine(CurrentBuildPanelCoroutine);
            GameManager.FPSCursorLock(true);
            m_Panel.SetActive(false);
        }
        public void Accept(Quest quest, UIQuestBoard board)
        {
            m_QuestManger.value.RegisterQuest(quest);
            board.OnQuestActive();
        }
    }
}