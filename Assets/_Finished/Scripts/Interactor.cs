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
        Camera m_Camera;
        private void Start()
        {
            m_Camera = GetComponentInChildren<Camera>();

        }
        private void Update()
        {
            TextElement infoText = UIManager.m_Instance.GetText("_INTERACTION");

            infoText.m_TextUI.text = "";
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Length, m_LayerMask))
            {
                if (hit.transform.CompareTag(m_Tag))
                {
                    Interactable obj = InteractionManager.m_Instance.GetInteractable(hit.transform.GetInstanceID());

                    if (infoText == null)
                        return;
                    infoText.m_TextUI.text = $"當前物件 : {0}, {obj.m_ObjectName}";
                    infoText.m_TextUI.color = obj.m_TextColor;
                }
            }
        }
    }
}
