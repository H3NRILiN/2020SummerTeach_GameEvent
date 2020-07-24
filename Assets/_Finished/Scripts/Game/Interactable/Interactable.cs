using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ISUExample
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public string m_ObjectName;
        public Color m_TextColor = Color.white;
        public bool m_UseKeyPress = false;
        [SerializeField] GameEvent_Item m_Event;
        public Pickupable m_PickupScript;
        private void Reset()
        {
            var collider = GetComponent<Collider>();

            if (collider is MeshCollider)
            {
                ((MeshCollider)collider).convex = true;
            }
            this.tag = "Interactable";
        }
        void Start()
        {
            InteractionManager.m_Instance.Register(this);
        }

        // Update is called once per frame
        public void OnInteract()
        {
            Debug.Log($"與{m_ObjectName}互動");

            if (m_Event)
            {
                m_Event.Response(m_PickupScript.m_Item);
            }

            if (m_PickupScript)
            {
                m_PickupScript.OnPickup();
            }
        }

        private void OnDestroy()
        {
            InteractionManager.m_Instance.UnRegister(this);
        }
    }
}
