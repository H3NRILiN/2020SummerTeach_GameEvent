namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using DG.Tweening;

    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] Vector3 m_CameraOffset;
        [SerializeField] Transform m_CameraFollowing;
        [SerializeField] bool m_IsFollowing = true;

        public CameraAnimationTarget m_BackBasicTarget;
        public static CameraAnimation instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError($"場上已經有一個CameraAnimation存在");
            }
            else
            {
                instance = this;
            }
        }

        private void Update()
        {
            ProcessCameraFollowing();
        }

        public void MoveToTarget(CameraAnimationTarget target)
        {
            if (target == null)
                return;

            m_IsFollowing = false;

            target.MoveTo();
        }

        public void ProcessCameraFollowing()
        {
            if (m_IsFollowing)
            {
                transform.position = GetCameraFollowingPosition();
                transform.rotation = m_CameraFollowing.rotation;
            }
        }

        Vector3 GetCameraFollowingPosition() => m_CameraFollowing.position + m_CameraOffset;

        public void SetFollowing(bool follow) => m_IsFollowing = follow;


    }
}