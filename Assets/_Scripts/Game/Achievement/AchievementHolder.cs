using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using ISU.Example;

public class AchievementHolder : MonoBehaviour
{
    [SerializeField] GameEvent m_OnAchievementRegister;
    [SerializeField] AchievementVariable m_RegisteredAchivement;
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
        m_RegisteredAchivement.value = m_Achievements[index];
        m_OnAchievementRegister.Raise();
    }

    public void RegisterAllAchievement()
    {
        for (int i = 0; i < m_Achievements.Length; i++)
        {
            RegisterAchievementAt(i);
        }
    }


}
