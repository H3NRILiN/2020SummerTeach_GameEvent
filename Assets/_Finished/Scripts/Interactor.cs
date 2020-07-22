using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISUExample
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] LayerMask m_LayerMask;
        [SerializeField] float m_Length;
        [SerializeField] string m_Tag;

        [SerializeField] Text m_UIText;
        Camera m_Camera;
        private void Start()
        {
            m_Camera = GetComponentInChildren<Camera>();
        }
        private void Update()
        {
            m_UIText.text = "";
            RaycastHit hit;
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_Length, m_LayerMask))
            {
                if (hit.transform.CompareTag(m_Tag))
                {
                    Interactable obj = InteractionManager.m_Instance.Access(hit.transform.GetInstanceID());
                    m_UIText.text = "當前物件 : " + obj.m_ObjectName;
                    m_UIText.color = obj.m_TextColor;
                }
            }
        }
    }
}
