namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using DG.Tweening;

    /// <summary>
    /// 攝影機動畫用
    /// </summary>
    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] Vector3 m_Offset; //偏移
        [SerializeField] Transform m_FollowingTarget; //基礎目標(CameraPostion)
        [SerializeField] bool m_IsFollowing = true; //正在跟隨基礎目標

        public CameraAnimationTarget m_BackBasicTarget; //基本動畫目標(回歸到原位時的位置)

        #region Singleton
        public static CameraAnimation Instance
        {
            get
            {
                if (!instance)
                {
                    Debug.LogError("場上沒有CameraAnimation腳本");
                }
                return instance;
            }
            private set
            {

            }
        } //Singleton
        static CameraAnimation instance;
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
        #endregion

        private void Update()
        {
            ProcessCameraFollowing();
        }

        /// <summary>
        /// 位移到動畫目標：取消跟隨基礎目標，而跟隨動畫目標
        /// </summary>
        /// <param name="target">動畫目標</param>
        public void MoveToTarget(CameraAnimationTarget target)
        {
            if (target == null)
                return;

            m_IsFollowing = false;

            target.MoveTo();
        }

        /// <summary>
        /// 跟隨基礎目標
        /// </summary>
        public void ProcessCameraFollowing()
        {
            if (m_IsFollowing)
            {
                transform.position = GetCameraFollowingPosition();
                transform.rotation = m_FollowingTarget.rotation;
            }
        }

        /// <summary>
        /// 獲取基礎目標+偏移
        /// </summary>
        /// <returns></returns>
        Vector3 GetCameraFollowingPosition() => m_FollowingTarget.position + m_Offset;

        /// <summary>
        /// 外部設置是否跟隨基礎目標
        /// </summary>
        /// <param name="follow">是否跟隨</param>
        public void SetFollowing(bool follow) => m_IsFollowing = follow;


    }
}