using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ISU.Common;

namespace ISU.Example
{
    public class UIQuestPanel : MonoBehaviour
    {
        [SerializeField] GameObject m_Panel;
        [SerializeField] RectTransform m_MissionBoardContainer;
        [SerializeField] VerticalLayoutGroup m_ContainerLayout;
        [SerializeField] ContentSizeFitter m_ContainerSizeFittter;
        [SerializeField] UIQuestBoard m_MissionBoardPrefab;
        [SerializeField] GameEventQuestPair m_OnQuestAccept;

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
        void PanelOpen(QuestVariable[] quests)
        {
            if (CurrentBuildPanelCoroutine != null)
                StopCoroutine(CurrentBuildPanelCoroutine);
            CurrentBuildPanelCoroutine = BuildPanelContent(quests);
            StartCoroutine(CurrentBuildPanelCoroutine);
        }

        IEnumerator BuildPanelContent(QuestVariable[] quests)
        {
            foreach (var existBoard in m_MissionBoardContainer.GetComponentsInChildren<UIQuestBoard>())
            {
                Destroy(existBoard.gameObject);
            }

            yield return SetLayout(true);

            GameState.m_PlayerCursorLock(false);
            GameState.m_PlayerCanMove(false);
            m_Panel.SetActive(true);

            float fadeDuration = 0.1f;
            WaitForSeconds buildDelay = new WaitForSeconds(fadeDuration);

            for (int i = 0; i < quests.Length; i++)
            {
                Quest curQuest = quests[i].value;
                var board = Instantiate(m_MissionBoardPrefab, m_MissionBoardContainer);
                board.SetInfos(curQuest, () => Accept(curQuest, board), fadeDuration);
                yield return buildDelay;
            }
            yield return SetLayout(false);
        }

        IEnumerator SetLayout(bool on)
        {
            m_ContainerSizeFittter.enabled = on;
            m_ContainerLayout.enabled = on;
            yield return null;
        }

        public void Exit()
        {
            if (CurrentBuildPanelCoroutine != null)
                StopCoroutine(CurrentBuildPanelCoroutine);
            GameState.m_PlayerCursorLock(true);
            GameState.m_PlayerCanMove(true);
            m_Panel.SetActive(false);
        }
        public void Accept(Quest quest, UIQuestBoard board)
        {
            m_OnQuestAccept.Raise(quest);
            board.OnQuestActive();
        }
    }
}