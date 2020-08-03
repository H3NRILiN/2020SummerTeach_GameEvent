using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement : ISerializationCallbackReceiver
{
    public string name;
    public string achievementIDName;

    public ItemObject requiredItem;
    public AchievementStage[] stages;
    public int currentCount;

    public void AddCount(int amount, Action<AchievementStage> onComplete = null)
    {
        if (AllComplete())
            return;

        currentCount += amount;

        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].CompareCounts(currentCount, onComplete);
        }
    }

    bool AllComplete()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if (!stages[i].isCompleted)
            {
                return false;
            }
        }

        return true;
    }

    public void OnBeforeSerialize()
    {

        if (achievementIDName != null)
            achievementIDName = Helper.EngNameRegex.Replace(achievementIDName, "");

        if (stages != null)
            foreach (var stage in stages)
            {
                if (stage.name != null)
                {
                    stage.name = Helper.TitleRegex.Replace(stage.namePattern, name);
                    stage.name = Helper.CountRegex.Replace(stage.name, stage.goalCount.ToString());
                }
                if (stage.description != null)
                {
                    stage.description = Helper.TitleRegex.Replace(stage.descriptionPattern, name);
                    stage.description = Helper.CountRegex.Replace(stage.description, stage.goalCount.ToString());
                }
            }
    }

    public void OnAfterDeserialize()
    {
    }
}
[Serializable]
public class AchievementStage
{

    [Header("名字")]
    [ReadOnly] public string name;
    public string namePattern = "{t}";
    [Header("說明")]
    [ReadOnly] public string description;
    [TextArea] public string descriptionPattern = "{t}";
    public int goalCount;
    public ItemObject goalReward;
    // public bool isActive;
    public bool isCompleted;

    public bool CompareCounts(int count, Action<AchievementStage> onComplete)
    {
        if (count >= goalCount)
        {
            onComplete(this);
            isCompleted = true;
            return true;
        }
        return false;
    }
}
public enum QuestType
{
    Mission,
    Acheivement
}