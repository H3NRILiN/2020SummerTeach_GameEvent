using UnityEngine;

namespace ISU.Example
{
    public class AchievementHolder : MonoBehaviour
    {
        [SerializeField] GameEventAchivementPair m_OnAchievementAdd;
        [SerializeField] Achievement[] m_Achievements;
        private void Start()
        {
            for (int i = 0; i < m_Achievements.Length; i++)
            {
                RegisterAchievementAt(i);
            }
        }

        public void RegisterAchievementAt(int index)
        {
            m_OnAchievementAdd.Raise(m_Achievements[index]);
        }

        public void RegisterAllAchievement()
        {
            for (int i = 0; i < m_Achievements.Length; i++)
            {
                RegisterAchievementAt(i);
            }
        }


    }
}