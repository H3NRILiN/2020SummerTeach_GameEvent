using System;
using System.Collections;
using System.Collections.Generic;
using ISU.Example;
using ISU.Example.Events;
using UnityEngine;
public class QuestHolder : MonoBehaviour
{
    [SerializeField] GameEventQuestPair m_OnQuestRegister;
    public static Action<QuestVariable[]> m_OnQuestPanelOpen;
    [SerializeField] QuestVariable[] m_Quests;
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
        m_OnQuestRegister.Raise(m_Quests[index].value);
    }

    private void OnDisable()
    {
        foreach (var quest in m_Quests)
        {
            quest.value.ResetData();
        }
    }
}
