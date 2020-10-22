using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.GameEvent
{
    public class ObjectPointerUI : MonoBehaviour
    {
        [SerializeField] Transform m_Pointer;
        [SerializeField] Vector3 m_Offset;
        Transform m_CurrentObject;
        Camera m_Camera;

        private void Start()
        {
            m_Camera = Camera.main;
        }
        public void OnTransformChange(HandHoldObject obj)
        {
            m_CurrentObject = obj.transform;
        }
        public void OnTransformClear()
        {
            m_CurrentObject = null;
        }

        private void Update()
        {
            if (m_CurrentObject)
            {
                m_Pointer.gameObject.SetActive(true);
                m_Pointer.position = m_Camera.WorldToScreenPoint(m_CurrentObject.position + m_Offset);
            }
            else
            {
                m_Pointer.gameObject.SetActive(false);
            }
        }
    }
}