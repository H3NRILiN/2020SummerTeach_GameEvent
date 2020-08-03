using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class AchievementHolder : MonoBehaviour
{
    [SerializeField] AchievementManagerVariable m_AchievementManager;
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
        m_AchievementManager.value.RegisterQuest(m_Achievements[index]);
    }

    public void RegisterAllAchievement()
    {
        for (int i = 0; i < m_Achievements.Length; i++)
        {
            RegisterAchievementAt(i);
        }
    }


}
