using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Observer : MonoBehaviour
{
    [SerializeField] string m_ObjectName = "物件";
    // Start is called before the first frame update
    [SerializeField] ConnectionLine m_Line;

    bool m_SenderAssign;

    Tween m_ProcessTween;

    private void OnEnable()
    {
        EventSender.m_OnEventSend += ReciveEvent;
        EventSender.m_OnRegisterEvent += RegisterEvent;
        EventSender.m_OnEventReset += ResetEvent;
        m_Line.m_EndPoint = transform;
    }

    private void OnDisable()
    {
        // EventSender.m_OnEventSend -= ReciveEvent;
        // EventSender.m_OnRegisterEvent -= RegisterEvent;
        // EventSender.m_OnEventReset -= ResetEvent;
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
