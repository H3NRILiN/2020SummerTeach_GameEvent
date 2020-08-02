﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [SelectionBase]
    public class Interactable : MonoBehaviour
    {
        [SerializeField] InteractionManager m_InteractionManager;
        public string m_ObjectName;
        //顏色 (用於字等等的)
        public Color m_TextColor = Color.white;
        //使用按鍵互動?
        public bool m_UseKeyPress = false;
        //互動時觸發事件
        [SerializeField] UnityEvent m_Event;

        public SubInteractor m_SubInteractor;

        public void Initialize(Interactable interactable)
        {
            m_InteractionManager = interactable.m_InteractionManager;
            m_ObjectName = interactable.m_ObjectName;
            m_TextColor = interactable.m_TextColor;
            m_UseKeyPress = interactable.m_UseKeyPress;
            m_Event = interactable.m_Event;
            m_SubInteractor = interactable.m_SubInteractor;
        }

        private void Reset()
        {
            //自動設置Tag
            this.tag = "Interactable";
        }
        void Start()
        {
            //註冊進Interaction Manager
            m_InteractionManager.Register(this);
        }

        //開始互動
        public void OnInteract()
        {
            //Debug.Log($"與{m_ObjectName}互動");

            //觸發事件
            m_Event.Invoke();

            if (m_SubInteractor != null)
            {
                //觸發Pickup動作
                m_SubInteractor.OnInteract();
            }
        }

        private void OnDestroy()
        {
            //取消註冊
            m_InteractionManager.UnRegister(this);
        }

        private void OnDrawGizmosSelected()
        {
            var eventCount = m_Event.GetPersistentEventCount();
            if (eventCount > 0)
            {
                for (int i = 0; i < eventCount; i++)
                {
                    if (m_Event.GetPersistentTarget(i) && m_Event.GetPersistentTarget(i) is Component)
                    {
                        DrawGizmosLine(((Component)m_Event.GetPersistentTarget(i)).transform.position);
                    }

                    if (m_Event.GetPersistentTarget(i) && m_Event.GetPersistentTarget(i) is GameEventCore)
                    {
                        foreach (var gl in Resources.FindObjectsOfTypeAll(typeof(GameEventListenerCore)))
                        {
                            if (((GameEventListenerCore)gl).m_Event == (GameEventCore)m_Event.GetPersistentTarget(i))
                            {
                                DrawGizmosLine(((GameEventListenerCore)gl).transform.position);
                            }
                        }
                    }
                }
            }
        }

        private void DrawGizmosLine(Vector3 pos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, pos);
            Gizmos.DrawSphere(pos, .05f);
        }
    }
}
