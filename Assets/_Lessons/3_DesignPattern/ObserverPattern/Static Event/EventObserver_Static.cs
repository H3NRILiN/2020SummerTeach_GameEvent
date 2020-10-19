using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace ISU.Lesson.DesignPattern.Observer
{
    public class EventObserver_Static : MonoBehaviour
    {
        [SerializeField] string m_ObjectName = "物件";

        [SerializeField] ConnectionLine m_Line;

        [SerializeField] SpriteRenderer m_Sprite;

        bool m_SenderAssign;

        Tween m_ProcessTween;

        private void OnEnable()
        {
            EventSender_Static.m_OnEventSend += ReceiveEvent;
            EventSender_Static.m_OnRegisterEvent += RegisterEvent;
            EventSender_Static.m_OnEventReset += ResetEvent;
            m_Line.m_EndPoint = transform;
        }

        private void OnDisable()
        {
            EventSender_Static.m_OnEventSend -= ReceiveEvent;
            EventSender_Static.m_OnRegisterEvent -= RegisterEvent;
            EventSender_Static.m_OnEventReset -= ResetEvent;
        }

        void Start()
        {
        }

        private void Update()
        {
        }

        void ReceiveEvent(Color color)
        {
            if (!m_ProcessTween.IsActive())
            {
                m_Line.m_ProcessColor = color;
                m_ProcessTween = DOTween.To(() => m_Line.m_Process, x => m_Line.m_Process = x, 1, 0.5f)
                .OnComplete(() =>
                {
                    Debug.Log($"名稱:{m_ObjectName}");
                    m_Sprite.color = color;
                });
            }

        }

        void RegisterEvent(Transform position)
        {
            m_Line.m_StartPoint = position;
            ResetEvent();
        }

        void ResetEvent()
        {
            if (m_ProcessTween.IsActive())
                m_ProcessTween.Kill();
            m_Line.m_Process = 0;
        }
    }
}