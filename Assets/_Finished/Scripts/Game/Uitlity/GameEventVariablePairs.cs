using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ISU.Example.Events;
using ISU.Example.Inventory;

namespace ISU.Example
{
    [Serializable]
    public class GameEventQuestPair
    {
        [SerializeField] GameEvent gameEvent;
        [SerializeField] QuestVariable quest;

        public void Raise(Quest quest)
        {
            this.quest.value = quest;
            gameEvent.Raise();
        }
    }


    [Serializable]
    public class GameEventAchivementPair
    {
        [SerializeField] GameEvent gameEvent;
        [SerializeField] AchievementVariable achievement;

        public void Raise(Achievement achievement)
        {
            this.achievement.value = achievement;
            gameEvent.Raise();
        }
    }

    [Serializable]
    public class GameEventIntItemPairPair
    {
        [SerializeField] GameEvent gameEvent;
        [SerializeField] IntItemPairVariable intItem;

        public void Raise(ItemObject item, int amount)
        {
            this.intItem.value.itemValue = item;
            this.intItem.value.intValue = amount;
            gameEvent.Raise();
        }
    }

}