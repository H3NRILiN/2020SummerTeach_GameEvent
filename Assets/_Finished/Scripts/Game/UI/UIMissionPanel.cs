using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIMissionPanel : MonoBehaviour
    {
        [SerializeField] GameObject m_Panel;
        [SerializeField] RectTransform m_MissionBoardContainer;
        [SerializeField] VerticalLayoutGroup m_ContainerLayout;
        [SerializeField] ContentSizeFitter m_ContainerSizeFittter;
        [SerializeField] UIMissionBoard m_MissionBoardPrefab;

        IEnumerator CurrentBuildPanelCoroutine;
        private void Start()
        {
            QuestHolder.m_OnQuestPanelOpen += PanelOpen;
            m_Panel.SetActive(false);
        }

        void PanelOpen(Quest[] quests)
        {
            CurrentBuildPanelCoroutine = BuildPanelContent(quests);
            StartCoroutine(CurrentBuildPanelCoroutine);
        }

        IEnumerator BuildPanelContent(Quest[] quests)
        {
            foreach (var existBoard in m_MissionBoardContainer.GetComponentsInChildren<UIMissionBoard>())
            {
                Destroy(existBoard.gameObject);
            }

            AutoLayout(true);
            yield return null;

            GameManager.FPSCursorLock(false);
            m_Panel.SetActive(true);

            WaitForSeconds buildDelay = new WaitForSeconds(0.05f);

            for (int i = 0; i < quests.Length; i++)
            {
                yield return buildDelay;
                Quest curQuest = quests[i];
                var board = Instantiate(m_MissionBoardPrefab, m_MissionBoardContainer);
                board.SetInfos(curQuest, () => Accept(curQuest, board));
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
        public void Accept(Quest quest, UIMissionBoard board)
        {
            QuestManager.m_Instance.RegisterQuest(quest);
            board.OnQuestActive();
        }
    }
}