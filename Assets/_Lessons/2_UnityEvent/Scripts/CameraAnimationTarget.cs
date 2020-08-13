using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CameraAnimationTarget : MonoBehaviour
{
    public Transform m_Position;
    public float m_Duration = 0.1f;
    public float m_WaitSeconds;
    public UnityEvent m_OnBegin;
    public UnityEvent m_OnEnd;

    public CameraAnimationTarget m_NextTarget;
}