using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : SubInteractor
    {
        [SerializeField] QuestManager m_QuestManger;
        [SerializeField] AchievementManager m_AchievementManager;
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
            m_AchievementManager.AddCount("A_Pickup");
            if (m_Item)
            {
                m_AchievementManager.AddCount(m_Item, m_ItemCount);
                m_QuestManger.AddCount(m_Item, m_ItemCount);
            }
            Destroy(gameObject);
        }
    }
}