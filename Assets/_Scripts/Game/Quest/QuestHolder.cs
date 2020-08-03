using System;
using System.Collections;
using System.Collections.Generic;
using ISU.Example;
using UnityEngine;
public class QuestHolder : MonoBehaviour
{
    [SerializeField] GameEvent m_OnQuestRegister;
    [SerializeField] QuestVariable m_RegisteredQuest;
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
        m_RegisteredQuest.value = m_Quests[index];
        m_OnQuestRegister.Raise();
    }

}
