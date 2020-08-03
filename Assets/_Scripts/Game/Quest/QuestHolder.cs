using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestHolder : MonoBehaviour
{
    [SerializeField] QuestManagerVariable m_QuestManger;
    public static Action<Quest[]> m_OnQuestPanelOpen;
    [SerializeField] Quest[] m_Quests;
    private void Start()
    {

    }
    public void OpenMissionMenu()
    {
        if (m_OnQuestPanelOpen != null)
            m_OnQuestPanelOpen(m_Quests);
    }
    public void RegisterQuestAt(int index)
    {
        m_QuestManger.value.RegisterQuest(m_Quests[index]);
    }

}
