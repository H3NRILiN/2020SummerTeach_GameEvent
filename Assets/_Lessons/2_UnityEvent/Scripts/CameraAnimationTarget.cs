namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using UnityEngine.Events;
    using DG.Tweening;

    public class CameraAnimationTarget : MonoBehaviour
    {
        [SerializeField] float m_Duration = 0.1f;
        [SerializeField] float m_DelaySeconds;
        [SerializeField] float m_WaitSeconds;
        [SerializeField] Transform m_TargetPosition;
        [SerializeField] Ease m_Ease;
        [Header("開始時執行")]
        [SerializeField] UnityEvent m_OnBegin;
        [SerializeField] bool m_SetFollowOnBegin;
        [SerializeField] bool m_SetCursorLockOnBegin;
        [SerializeField] bool m_SetCanMoveOnBegin;
        [Header("結束時執行")]
        [SerializeField] UnityEvent m_OnEnd;
        [SerializeField] bool m_SetFollowOnEnd;
        [SerializeField] bool m_SetCursorLockOnEnd;
        [SerializeField] bool m_SetCanMoveOnEnd;

        IEnumerator m_MoveCoroutine;

        CameraAnimation m_CameraAnim;
        private void Start()
        {
            m_CameraAnim = CameraAnimation.instance;
            if (m_CameraAnim == null)
                Debug.LogError("場上沒有CameraAnimation腳本");
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
            //插入位移tween
            sequence.Insert(0, m_CameraAnim.transform.DOMove(target.position, m_Duration, false)
            .SetEase(m_Ease)
            .SetDelay(m_DelaySeconds));
            //插入旋轉tween
            sequence.Insert(0, m_CameraAnim.transform.DORotate(target.rotation.eulerAngles, m_Duration)
            .SetEase(m_Ease)
            .SetDelay(m_DelaySeconds));
            //
            yield return sequence.WaitForCompletion();

            WaitForSeconds waitForEnd = new WaitForSeconds(m_WaitSeconds);
            yield return waitForEnd;

            m_OnEnd.Invoke();
            m_CameraAnim.SetFollowing(m_SetFollowOnEnd);
        }

        public void BackToBase()
        {
            m_CameraAnim.m_BackBasicTarget.MoveTo();
        }

    }
}