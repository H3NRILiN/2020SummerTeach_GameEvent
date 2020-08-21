using System.Collections;
using System.Collections.Generic;
using ISU.Example.Inventory;
using UnityEngine;

namespace ISU.Example
{
    public class AchievementManager : MonoBehaviour
    {
        List<Achievement> m_Achievements;
        [SerializeField] bool m_DebugMode;

        private void Awake()
        {
            m_Achievements = new List<Achievement>();
        }

        public void RegisterAchievement(Achievement ach)
        {
            if (!m_Achievements.Contains(ach))
            {
                if (m_DebugMode) Debug.Log($"註冊成就 :{ach.name}");
                m_Achievements.Add(ach);
            }
        }

        public void RegisterAchievementByEvent(AchievementVariable regesteredAchivement)
        {
            RegisterAchievement(regesteredAchivement.value);
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
                if (m_DebugMode) Debug.Log($"成就: {stage.name}完成");
        }

        public void AddCountByEvent(IntItemPairVariable itemWhithCount)
        {
            AddCount(itemWhithCount.value.itemValue, itemWhithCount.value.intValue);
        }
    }
}