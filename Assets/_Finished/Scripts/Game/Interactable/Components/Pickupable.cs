using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISU.Example.Events;
using ISU.Example.Inventory;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : SubInteractor, ISerializationCallbackReceiver
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

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            // var interactable = GetComponent<Interactable>();
            // if (interactable)
            // {
            //     if (interactable.m_UseItemData)
            //     {
            //         interactable.m_ObjectName = m_Item.m_ItemName;
            //         interactable.m_TextColor = m_Item.m_DisplayColor;
            //     }
            // }
        }
    }
}