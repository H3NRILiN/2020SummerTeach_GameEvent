using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "_Finished/AchievementManager")]
public class AchievementManager : ScriptableObject
{
    List<Achievement> m_Achievements;
    [SerializeField] bool m_Debug;

    private void OnEnable()
    {
        m_Achievements = new List<Achievement>();
    }

    public void RegisterQuest(Achievement ach)
    {
        if (!m_Achievements.Contains(ach))
        {
            if (m_Debug) Debug.Log($"註冊成就 :{ach.name}");
            m_Achievements.Add(ach);
        }
    }

    public void AddCount(ItemObject item, int amount)
    {
        Achievement ach;
        for (int i = 0; i < m_Achievements.Count; i++)
        {
            ach = m_Achievements[i];
            if (!ach.requiredItem)
                continue;
            if (ach.requiredItem.GetID() == item.GetID())
            {
                ach.AddCount(amount, (x) => AchievementComplete(ach, x));
            }
        }
    }

    public void AddCount(ItemObject item)
    {
        AddCount(item, 1);
    }
    public void AddCount(string IDName, int amount)
    {
        Achievement ach;
        for (int i = 0; i < m_Achievements.Count; i++)
        {
            ach = m_Achievements[i];
            if (ach.achievementIDName == IDName)
            {
                ach.AddCount(amount, (x) => AchievementComplete(ach, x));
            }
        }
    }
    public void AddCount(string IDName)
    {
        AddCount(IDName, 1);
    }

    void AchievementComplete(Achievement achievement, AchievementStage stage)
    {
        if (!stage.isCompleted)
            Debug.Log($"成就: {stage.name}完成");
    }
}
