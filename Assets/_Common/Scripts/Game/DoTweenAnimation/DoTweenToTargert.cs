using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DoTweenToTargert : MonoBehaviour
{
    [SerializeField] Transform m_MovingObject;
    [SerializeField] Transform m_TargetLocation;

    [SerializeField] float m_Duration;

    [SerializeField] Ease m_EaseType;
    [SerializeField] bool m_UseRotate;

    [SerializeField] UnityEvent m_OnMoveToComplete;
    [SerializeField] UnityEvent m_OnMoveBackToComplete;

    Vector3 m_OriginalPosition;
    Vector3 m_OriginalRotationAngle;

    Sequence m_CurrentSequence;
    public void MoveTo()
    {
        m_CurrentSequence = DOTween.Sequence();
        m_OriginalPosition = m_MovingObject.position;


        m_CurrentSequence.Insert(0, m_MovingObject.DOMove(m_TargetLocation.position, m_Duration).SetEase(m_EaseType));
        if (m_UseRotate)
        {
            m_OriginalRotationAngle = m_MovingObject.rotation.eulerAngles;
            m_CurrentSequence.Insert(0, m_MovingObject.DORotate(m_TargetLocation.rotation.eulerAngles, m_Duration).SetEase(m_EaseType));
        }
        m_CurrentSequence.OnComplete(() => m_OnMoveToComplete.Invoke());
    }

    public void MoveBack()
    {
        m_CurrentSequence = DOTween.Sequence();

        m_CurrentSequence.Insert(0, m_MovingObject.DOMove(m_OriginalPosition, m_Duration).SetEase(m_EaseType)).SetDelay(0);

        if (m_UseRotate) m_CurrentSequence.Insert(0, m_MovingObject.DORotate(m_OriginalRotationAngle, m_Duration).SetEase(m_EaseType)).SetDelay(0);

        m_CurrentSequence.OnComplete(() => m_OnMoveBackToComplete.Invoke());
    }

    public void DebugText(string text)
    {
        Debug.Log(text);
    }
}
