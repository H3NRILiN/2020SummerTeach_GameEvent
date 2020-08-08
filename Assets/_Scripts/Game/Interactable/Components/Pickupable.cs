using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISU.Example.Events;
using ISU.Example.Inventory;
using ISU.Example;

namespace ISU.Common
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : SubInteractor
    {
        // [SerializeField] QuestManager m_QuestManger;
        // [SerializeField] AchievementManager m_AchievementManager;
        [SerializeField] GameEventIntItemPairPair m_OnItemPickup;
        public ItemObject m_Item;
        public int m_ItemCount = 1;

        private void Reset()
        {
            var collider = GetComponent<Collider>();

            if (collider is MeshCollider)
            {
                ((MeshCollider)collider).convex = true;
            }

            collider.isTrigger = true;
        }

        public override void OnInteract()
        {
            // m_AchievementManager.AddCount("A_Pickup");
            if (m_Item)
            {
                m_OnItemPickup.Raise(m_Item, m_ItemCount);
                // m_AchievementManager.AddCount(m_Item, m_ItemCount);
                // m_QuestManger.AddCount(m_Item, m_ItemCount);
            }
            Destroy(gameObject);
        }
    }
}