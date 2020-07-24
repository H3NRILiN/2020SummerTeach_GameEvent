using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISUExample
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Interactable))]
    public class Pickupable : MonoBehaviour
    {
        public GameItem m_Item;
        private void Reset()
        {
            var collider = GetComponent<Collider>();

            if (collider is MeshCollider)
            {
                ((MeshCollider)collider).convex = true;
            }

            collider.isTrigger = true;

            GetComponent<Interactable>().m_PickupScript = this;
        }

        public void OnPickup()
        {
            Debug.Log("撿起");
            Destroy(gameObject);
        }
    }
}