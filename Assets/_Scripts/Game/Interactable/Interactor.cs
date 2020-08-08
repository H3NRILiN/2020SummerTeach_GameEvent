using System.Collections;
using System.Collections.Generic;
using ISU.Example;
using UnityEngine;

namespace ISU.Common
{
    public class Interactor : MonoBehaviour
    {
        const int m_NullInstanceID = 0;
        [SerializeField] InteractionManager m_InteractionManager;
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] float m_InteractDistance;
        UIInteractionPanel m_NoticeUI;
        Camera m_Camera;

        int m_CurrentFocusingID = -1;
        int m_LastFocusedID;
        Interactable m_CurrentFocusing;
        [SerializeField] bool m_Debug;
        private void Start()
        {

            m_Camera = GetComponentInChildren<Camera>();
            m_NoticeUI = FindObjectOfType<UIInteractionPanel>();
            OnEndFocus();
        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_InteractDistance, m_LayerMask))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    Interactable interactable = m_InteractionManager.GetInteractable(hit.transform.GetInstanceID());
                    if (interactable == null) return;
                    m_CurrentFocusingID = interactable.transform.GetInstanceID();
                    m_CurrentFocusing = interactable;
                }
                else
                {
                    m_CurrentFocusingID = m_NullInstanceID;
                    m_CurrentFocusing = null;
                }
            }
            else
            {
                m_CurrentFocusingID = m_NullInstanceID;
                m_CurrentFocusing = null;
            }


            if (m_CurrentFocusingID != m_NullInstanceID)
            {
                if (m_LastFocusedID != m_CurrentFocusingID)
                {
                    OnBeginFocus();
                }
                OnFocusing();
            }

            if (m_CurrentFocusingID == m_NullInstanceID)
            {
                if (m_CurrentFocusingID != m_LastFocusedID)
                {
                    OnEndFocus();
                }
            }

            m_LastFocusedID = m_CurrentFocusingID;
        }
        void OnBeginFocus()
        {
            if (m_Debug) Debug.Log("<color=lime>OnBeginFocus</color>");
            m_NoticeUI.Show(m_CurrentFocusing.m_ObjectName
            , m_CurrentFocusing.m_TextColor
            , m_CurrentFocusing.m_UseKeyPress, "E");
        }
        void OnFocusing()
        {
            // Debug.Log("OnFocusing");
            if (m_CurrentFocusing.m_UseKeyPress && Input.GetButtonDown("Interact"))
            {
                OnInteract();
            }
        }

        void OnEndFocus()
        {
            if (m_Debug) Debug.Log("<color=red>OnEndFocus</color>");
            m_NoticeUI.Hide();
        }

        void OnInteract()
        {
            if (m_Debug) Debug.Log("<color=aqua>OnInteract</color>");
            Interact();
        }



        private void Interact()
        {
            m_CurrentFocusing.OnInteract();
            //m_OnInteract.Raise();
            // if (m_CurrentFocusing.m_SubInteractor
            // && m_CurrentFocusing.m_SubInteractor.GetType() == typeof(Pickupable))
            // {
            //     m_OnPickup.Raise();
            // }
        }
    }
}
