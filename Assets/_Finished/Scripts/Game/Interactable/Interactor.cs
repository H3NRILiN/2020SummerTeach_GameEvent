using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] float m_InteractDistance;


        [SerializeField] GameEvent m_OnInteract;
        [SerializeField] GameEvent m_OnPickup;

        UIInteractionPanel m_NoticeUI;
        Camera m_Camera;
        private void Start()
        {
            m_Camera = GetComponentInChildren<Camera>();
            m_NoticeUI = FindObjectOfType<UIInteractionPanel>();
        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_InteractDistance, m_LayerMask))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    Interact(InteractionManager.m_Instance.GetInteractable(hit.transform.GetInstanceID()));
                }
                else
                {
                    m_NoticeUI.Hide();
                }
            }
            else
            {
                m_NoticeUI.Hide();
            }
        }

        private void Interact(Interactable inteObj)
        {
            if (inteObj == null) return;
            m_NoticeUI.Show(inteObj.m_ObjectName, inteObj.m_TextColor, inteObj.m_UseKeyPress, "E");

            if (inteObj.m_UseKeyPress && Input.GetButtonDown("Interact"))
            {
                inteObj.OnInteract();
                m_OnInteract.DoInvoke();
                if (inteObj.m_SubInteractor is Pickupable)
                {
                    m_OnPickup.DoInvoke();
                }
            }
        }
    }
}
