namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using DG.Tweening;

    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] Camera m_Camera;
        [SerializeField] Vector3 m_CameraOffset;
        [SerializeField] Transform m_CameraFollowing;
        [SerializeField] Interactor m_Interactor;
        [SerializeField] bool m_IsFollowing = true;


        public void MoveToTarget(CameraAnimationTarget target)
        {
            m_IsFollowing = false;
            Sequence sequence = DOTween.Sequence();
            target.m_OnBegin.Invoke();
            sequence.Insert(0, m_Camera.transform.DOMove(target.m_Position.position, target.m_Duration, false).SetDelay(target.m_WaitSeconds));
            sequence.Insert(0, m_Camera.transform.DORotate(target.m_Position.rotation.eulerAngles, target.m_Duration).SetDelay(target.m_WaitSeconds));
            sequence.OnComplete(() =>
            {
                target.m_OnEnd.Invoke();
                if (target.m_NextTarget != null)
                {
                    MoveToTarget(target.m_NextTarget);
                }
            });
        }

        public void ProcessCameraFollowing()
        {
            if (m_IsFollowing)
            {
                m_Camera.transform.position = GetCameraFollowingPosition();
                m_Camera.transform.rotation = m_CameraFollowing.rotation;
            }
        }

        Vector3 GetCameraFollowingPosition() => m_CameraFollowing.position + m_CameraOffset;
    }
}