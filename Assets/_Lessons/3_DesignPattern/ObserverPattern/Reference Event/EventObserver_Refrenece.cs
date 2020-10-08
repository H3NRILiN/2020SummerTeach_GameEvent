using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EventObserver_Refrenece : MonoBehaviour
{
    [SerializeField] string m_ObjectName = "物件";

    [SerializeField] ConnectionLine m_Line;

    bool m_SenderAssign;

    Tween m_ProcessTween;

    EventSender_Refrenece m_Sender;


    private void OnEnable()
    {
        m_Sender = FindObjectOfType<EventSender_Refrenece>();

        m_Sender.m_OnEventSend += ReciveEvent;
        m_Sender.m_OnRegisterEvent += RegisterEvent;
        m_Sender.m_OnEventReset += ResetEvent;
        m_Line.m_EndPoint = transform;
    }

    private void OnDisable()
    {
        // m_Sender.m_OnEventSend -= ReciveEvent;
        // m_Sender.m_OnRegisterEvent -= RegisterEvent;
        // m_Sender.m_OnEventReset -= ResetEvent;
    }

    void Start()
    {
    }

    private void Update()
    {
    }

    void ReciveEvent()
    {
        if (!m_ProcessTween.IsActive())
        {
            m_ProcessTween = DOTween.To(() => m_Line.m_Process, x => m_Line.m_Process = x, 1, 0.5f).
                      OnComplete(() => Debug.Log($"名稱:{m_ObjectName}"));
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
