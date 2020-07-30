using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : SubInteractor
    {
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
            AchievementManager.m_Instance.AddCount("A_Pickup");
            if (m_Item)
                AchievementManager.m_Instance.AddCount(m_Item, m_ItemCount);
            Destroy(gameObject);
        }
    }
}