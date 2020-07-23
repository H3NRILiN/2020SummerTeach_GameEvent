using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISUExample
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] float m_Length;
        [SerializeField] string m_Tag;
        [SerializeField] UIInteractionPanel m_NoticeUI;
        Camera m_Camera;
        private void Start()
        {
            m_Camera = GetComponentInChildren<Camera>();

        }
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Length, m_LayerMask))
            {
                if (hit.transform.CompareTag(m_Tag))
                {
                    Interactable obj = InteractionManager.m_Instance.GetInteractable(hit.transform.GetInstanceID());

                    m_NoticeUI.Show(obj.m_ObjectName, obj.m_TextColor, obj.m_UseKeyPress, "E");

                    if (obj.m_UseKeyPress && Input.GetButtonDown("Interact"))
                    {
                        obj.StartInteract();
                    }
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
    }
}
