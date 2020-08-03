using System;
using System.Collections;
using System.Collections.Generic;
using ISU.Example;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestManagerVariable m_Variable;
    [SerializeField] IntVariable m_TrackingQuestMax;
    [SerializeField] ListQuestVariable m_TrackingQuests;
    [SerializeField] GameEvent m_OnQuestAccept;
    [SerializeField] GameEvent m_OnAfterItemAdded;
    Dictionary<ItemObject, List<Quest>> m_ItemMatchedQuests;
    Dictionary<string, Quest> m_NameMatchedQuests;
    [SerializeField] bool m_DebugMode;

    private void Awake()
    {
        m_Variable.value = this;
        m_TrackingQuests.value = new List<Quest>();
        m_ItemMatchedQuests = new Dictionary<ItemObject, List<Quest>>();
        m_NameMatchedQuests = new Dictionary<string, Quest>();
    }


    /// <summary>
    /// 是否有此Quest
    /// </summary>
    /// <param name="quest"></param>
    /// <returns></returns>
    public bool Contains(Quest quest)
    {
        return m_NameMatchedQuests.ContainsKey(quest.name);
    }
    /// <summary>
    /// 註冊Quest
    /// </summary>
    /// <param name="quest"></param>
    public void RegisterQuest(Quest quest)
    {
        if (!m_ItemMatchedQuests.ContainsKey(quest.goal.requiredItem))
        {
            List<Quest> matchedQuests = new List<Quest>();
            matchedQuests.Add(quest);
            m_ItemMatchedQuests.Add(quest.goal.requiredItem, matchedQuests);
        }
        else
        {
            m_ItemMatchedQuests[quest.goal.requiredItem].Add(quest);
        }

        if (!m_NameMatchedQuests.ContainsKey(quest.name))
        {
            if (m_DebugMode) Debug.Log($"註冊任務 :{quest.name}");

            if (m_TrackingQuests.value.Count < m_TrackingQuestMax.value)
            {
                m_TrackingQuests.value.Add(quest);
            }

            if (m_DebugMode) Debug.Log($"tQuestC: {m_TrackingQuests.value.Count } | tQuestM: {m_TrackingQuestMax.value}");

            m_OnQuestAccept.Raise();
            m_NameMatchedQuests.Add(quest.name, quest);
            quest.active = true;
        }
    }

    /// <summary>
    /// 增加Quest的計量，依Item尋找Quest，增加amount
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void AddCount(ItemObject item, int amount)
    {

        if (m_ItemMatchedQuests.ContainsKey(item))
        {
            if (m_DebugMode) Debug.Log($"Add {item.m_ItemName} : {amount}");
            for (int i = 0; i < m_ItemMatchedQuests[item].Count; i++)
            {

                var quest = m_ItemMatchedQuests[item][i];
                quest.currentCount += amount;
            }
        }
    }

    /// <summary>
    /// 增加Quest的計量，依Item尋找Quest，增加1
    /// </summary>
    /// <param name="item"></param>
    public void AddCount(ItemObject item)
    {
        AddCount(item, 1);
    }

    /// <summary>
    ///  增加Quest的計量，依IDName尋找Quest，增加amount
    /// </summary>
    /// <param name="IDName"></param>
    /// <param name="amount"></param>
    public void AddCount(string IDName, int amount)
    {
        if (m_NameMatchedQuests.ContainsKey(IDName))
        {
            m_NameMatchedQuests[IDName].currentCount += amount;
        }
    }
    /// <summary>
    ///  增加Quest的計量，依IDName尋找Quest，增加1
    /// </summary>
    /// <param name="IDName"></param>
    public void AddCount(string IDName)
    {
        AddCount(IDName, 1);
    }

    public void AddCountByEvent(IntItemPairVariable itemWhithCount)
    {
        AddCount(itemWhithCount.value.itemValue, itemWhithCount.value.intValue);
        m_OnAfterItemAdded.Raise();
    }
}
