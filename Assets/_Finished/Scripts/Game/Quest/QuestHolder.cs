using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestHolder : MonoBehaviour
{
    public static Action<Quest[]> m_OnQuestPanelOpen;
    [SerializeField] Quest[] m_Quests;

    public void OpenMissionMenu()
    {
        if (m_OnQuestPanelOpen != null)
            m_OnQuestPanelOpen(m_Quests);
    }
    public void RegisterQuestAt(int index)
    {
        QuestManager.m_Instance.RegisterQuest(m_Quests[index]);
    }

}
