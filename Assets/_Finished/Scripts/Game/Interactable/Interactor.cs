using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example
{
    public class Interactor : MonoBehaviour
    {
        const int m_NullInstanceID = 0;
        [SerializeField] InteractionManager m_InteractionManager;
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] float m_InteractDistance;


        [SerializeField] GameEvent m_OnInteract;
        [SerializeField] GameEvent m_OnPickup;

        UIInteractionPanel m_NoticeUI;
        Camera m_Camera;

        int m_CurrentInteractingID = -1;
        int m_LastInteractedID;
        Interactable m_CurrentInteracting;
        private void Start()
        {

            m_Camera = GetComponentInChildren<Camera>();
            m_NoticeUI = FindObjectOfType<UIInteractionPanel>();
            OnEndInteract();
        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_InteractDistance, m_LayerMask))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    Interact(m_InteractionManager.GetInteractable(hit.transform.GetInstanceID()));
                }
                else
                {
                    m_CurrentInteractingID = m_NullInstanceID;
                    m_CurrentInteracting = null;
                }
            }
            else
            {
                m_CurrentInteractingID = m_NullInstanceID;
                m_CurrentInteracting = null;
            }


            if (m_CurrentInteractingID != m_NullInstanceID)
            {
                if (m_LastInteractedID != m_CurrentInteractingID)
                {
                    OnBeginInteract();
                }
                OnInteracting();
            }

            if (m_CurrentInteractingID == m_NullInstanceID)
            {
                if (m_CurrentInteractingID != m_LastInteractedID)
                {
                    OnEndInteract();
                }
            }

            m_LastInteractedID = m_CurrentInteractingID;
        }
        void OnBeginInteract()
        {
            Debug.Log("OnBeginInteract");
            m_NoticeUI.Show(m_CurrentInteracting.m_ObjectName
            , m_CurrentInteracting.m_TextColor
            , m_CurrentInteracting.m_UseKeyPress, "E");
        }
        void OnInteracting()
        {
            Debug.Log("OnInteract");
        }

        void OnEndInteract()
        {
            Debug.Log("OnEndInteract");
            m_NoticeUI.Hide();
        }




        private void Interact(Interactable inteObj)
        {
            if (inteObj == null) return;
            m_CurrentInteractingID = inteObj.transform.GetInstanceID();
            m_CurrentInteracting = inteObj;

            if (inteObj.m_UseKeyPress && Input.GetButtonDown("Interact"))
            {
                inteObj.OnInteract();
                m_OnInteract.DoInvoke();
                if (inteObj.m_SubInteractor && inteObj.m_SubInteractor.GetType() == typeof(Pickupable))
                {
                    m_OnPickup.DoInvoke();
                }
            }
        }
    }
}
