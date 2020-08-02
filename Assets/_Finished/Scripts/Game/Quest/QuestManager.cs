using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "_Finished/QuestManager")]
public class QuestManager : ScriptableObject
{
    Dictionary<ItemObject, List<Quest>> m_ItemMatchedQuests;
    Dictionary<string, Quest> m_NameMatchedQuests;

    private void OnEnable()
    {
        m_ItemMatchedQuests = new Dictionary<ItemObject, List<Quest>>();
        m_NameMatchedQuests = new Dictionary<string, Quest>();
    }
    public bool Contains(Quest quest)
    {
        return m_NameMatchedQuests.ContainsKey(quest.name);
    }
    public void RegisterQuest(Quest quest)
    {
        if (m_ItemMatchedQuests.ContainsKey(quest.goal.requiredItem))
        {
            m_ItemMatchedQuests[quest.goal.requiredItem].Add(quest);
        }
        else
        {
            List<Quest> matchedQuests = new List<Quest>();
            matchedQuests.Add(quest);
            m_ItemMatchedQuests.Add(quest.goal.requiredItem, matchedQuests);
        }

        if (!m_NameMatchedQuests.ContainsKey(quest.name))
        {
            Debug.Log($"註冊任務 :{quest.name}");
            m_NameMatchedQuests.Add(quest.name, quest);
            quest.active = true;
        }
    }

    public void AddCount(ItemObject item, int amount)
    {
        if (m_ItemMatchedQuests.ContainsKey(item))
        {
            for (int i = 0; i < m_ItemMatchedQuests[item].Count; i++)
            {
                var quest = m_ItemMatchedQuests[item][i];
                quest.currentCount += amount;
            }
        }
    }

    public void AddCount(ItemObject item)
    {
        AddCount(item, 1);
    }

    public void AddCount(string IDName, int amount)
    {
        if (m_NameMatchedQuests.ContainsKey(IDName))
        {
            m_NameMatchedQuests[IDName].currentCount += amount;
        }
    }

    public void AddCount(string IDName)
    {
        AddCount(IDName, 1);
    }
}
