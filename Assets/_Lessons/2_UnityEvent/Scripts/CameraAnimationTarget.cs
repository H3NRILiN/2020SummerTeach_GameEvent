namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using UnityEngine.Events;
    using DG.Tweening;
    using ISU.Common;
    using UnityEngine.Serialization;

    public class CameraAnimationTarget : MonoBehaviour
    {
        [SerializeField, Tooltip("攝影動畫執行時間")] float m_AnimationDuration = 0.1f;
        [SerializeField, Tooltip("動畫結束後的靜止時間")] float m_AnimaitonEndHoldSeconds;
        [SerializeField, Tooltip("動畫移動至目標")] Transform m_TargetPosition;
        [SerializeField, Tooltip("移動曲線")] Ease m_Ease;
        [Header("開始時執行")]
        [SerializeField, Tooltip("開始時觸發事件")] UnityEvent m_OnBegin;
        [SerializeField, Tooltip("在開始時是否返回基礎位置")] bool m_SetFollowOnBegin;
        [SerializeField, Tooltip("在開始時是否影藏滑鼠標")] bool m_SetCursorLockOnBegin;
        [SerializeField, Tooltip("在開始時玩家是否能移動")] bool m_SetCanMoveOnBegin;
        [Header("結束時執行")]
        [SerializeField, Tooltip("結束時觸發事件(靜止結束後)")] UnityEvent m_OnEnd;
        [SerializeField, Tooltip("在結束時是否返回基礎位置")] bool m_SetFollowOnEnd;
        [SerializeField, Tooltip("在結束時是否影藏滑鼠標")] bool m_SetCursorLockOnEnd;
        [SerializeField, Tooltip("在結束時玩家是否能移動")] bool m_SetCanMoveOnEnd;

        IEnumerator m_MoveCoroutine; //攝影機移動協程

        CameraAnimation m_CameraAnim; //攝影機移動主腳本參考
        private void Start()
        {
            m_CameraAnim = CameraAnimation.Instance;

        }
        //外部調用自訂值
        public void MoveTo(Transform target)
        {
            if (m_MoveCoroutine != null)
                StopCoroutine(m_MoveCoroutine);

            m_MoveCoroutine = MoveCoroutine(target);
            StartCoroutine(m_MoveCoroutine);
        }
        //外部調用內定值
        public void MoveTo()
        {
            MoveTo(m_TargetPosition);
        }
        //位移Routine
        IEnumerator MoveCoroutine(Transform target)
        {
            Sequence sequence = DOTween.Sequence();
            m_OnBegin.Invoke();
            m_CameraAnim.SetFollowing(m_SetFollowOnBegin);
            GameState.m_PlayerCursorLock(m_SetCursorLockOnBegin);
            GameState.m_PlayerCanMove(m_SetCanMoveOnBegin);

            //插入位移tween
            sequence.Insert(0, m_CameraAnim.transform.DOMove(target.position, m_AnimationDuration, false)
            .SetEase(m_Ease));
            //插入旋轉tween
            sequence.Insert(0, m_CameraAnim.transform.DORotate(target.rotation.eulerAngles, m_AnimationDuration)
            .SetEase(m_Ease));
            //等待所有Tween完成
            yield return sequence.WaitForCompletion();

            WaitForSeconds waitForEnd = new WaitForSeconds(m_AnimaitonEndHoldSeconds);
            yield return waitForEnd;

            m_OnEnd.Invoke();

            m_CameraAnim.SetFollowing(m_SetFollowOnEnd);
            GameState.m_PlayerCursorLock(m_SetCursorLockOnEnd);
            GameState.m_PlayerCanMove(m_SetCanMoveOnEnd);
        }

        public void BackToBase()
        {
            m_CameraAnim.m_BackBasicTarget.MoveTo();
        }

    }
}