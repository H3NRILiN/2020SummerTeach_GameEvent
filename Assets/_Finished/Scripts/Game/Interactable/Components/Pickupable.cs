using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISU.Example.Events;
using ISU.Example.Inventory;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : SubInteractor
    {
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

        private void Start()
        {
            var interacable = GetComponent<Interactable>();
            interacable.m_OnSubInteract = OnInteract;
            if (interacable)
            {
                if (interacable.m_UseItemData)
                {
                    interacable.m_ObjectName = m_Item.m_ItemName;
                    interacable.m_TextColor = m_Item.m_DisplayColor;
                }
            }
        }

        public override void OnInteract()
        {
            if (m_Item)
            {
                m_OnItemPickup.Raise(m_Item, m_ItemCount);
            }
            else
            {
                m_OnItemPickup.Raise(new ItemObject(), m_ItemCount);
            }
            Destroy(gameObject);
        }
    }
}