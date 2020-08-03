﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;

[Serializable]
public class Quest : ISerializationCallbackReceiver
{
    [Header("名字")]
    [ReadOnly] public string name;
    [SerializeField] string namePattern;
    [Header("說明")]
    [ReadOnly] public string description;
    [SerializeField] [TextArea] public string descriptionPattern;
    [Header("辨識ID")]
    public string IDName;
    public Goal goal;


    [Serializable]
    public class Goal
    {
        public string requiredQuestIDName;
        public ItemObject requiredItem;
        public int goalCount;
    }
    public int currentCount;
    public bool active;
    public bool complete;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        if (IDName != null && IDName.Length > 0)
        {
            IDName = Helper.EngNameRegex.Replace(IDName, "");
        }

        var requiredItemName = "";
        if (goal != null && goal.requiredItem)
            requiredItemName = goal.requiredItem.m_ItemName;
        if (name != null)
        {
            name = Helper.ItemNameRegex.Replace(namePattern, requiredItemName);
            name = Helper.CountRegex.Replace(name, goal.goalCount.ToString());
        }

        if (description != null)
        {
            description = Helper.CountRegex.Replace(description, goal.goalCount.ToString());
            description = Helper.ItemNameRegex.Replace(descriptionPattern, requiredItemName);
        }
    }
}