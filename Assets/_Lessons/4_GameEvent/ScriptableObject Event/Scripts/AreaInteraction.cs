using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ISU.Lesson.GameEvent
{
    public enum AreaInteractionState
    {
        PickFirst,
        PickSecond
    }
    public enum AreaInteractionType
    {
        Static,
        Pickup,
        Holding
    }

    //========================================================

    public class AreaInteraction : MonoBehaviour
    {
        [SerializeField] Transform m_DetectCenter;
        [SerializeField] float m_DetectRadius = 1;
        [SerializeField, Range(0, 1)] float m_DetectionYRange;
        [SerializeField] LayerMask m_DetectLayers = 9;

        //========================================================

        Vector3 m_DetectCenterP { get { return m_DetectCenter.position; } }
        AreaInteractionState m_CurrentState;
        AreaInteractObject m_CurrentClosestObject;
        AreaInteractObject m_LastClosestObject;
        AreaInteractObject m_CurrentInteracting;
        AreaInteractObject m_LastInteracting;
        Collider[] m_DetectedObjects = new Collider[10];

        float m_DetectRangeYTop { get => transform.position.y + m_DetectRadius; }
        float m_DetectRangeYBottom { get => transform.position.y - m_DetectRadius; }
        float m_DetectRangeY { get => Mathf.Lerp(m_DetectRangeYTop, m_DetectRangeYBottom, m_DetectionYRange); }

        public Action<AreaInteractObject> m_OnClosestObjectFound { get; set; } //有物件，或空值
        /// <summary>
        /// 互動，組合物件可有可無 (參數：當前物件|將組合物件)
        /// </summary>
        /// <value></value>
        public Action<AreaInteractObject, AreaInteractObject> m_OnInteract { get; set; }
        /// <summary>
        /// 取消互動 (參數：取消互動的物件)
        /// </summary>
        /// <value></value>
        public Action<AreaInteractObject> m_OnUnInteract { get; set; }

        //========================================================

        [Header("Debug")]

        [SerializeField] float m_ArrowWidth;
        [SerializeField, Range(0, 1)] float m_ArrowPosition;

        //========================================================

        private void Start()
        {
            if (!m_DetectCenter) m_DetectCenter = this.transform;
            m_CurrentState = AreaInteractionState.PickFirst;
        }

        private void Update()
        {
            m_CurrentClosestObject = FindColsestObject(m_CurrentState, m_CurrentInteracting);

            Interaction(m_CurrentClosestObject);

            if (m_LastClosestObject != m_CurrentClosestObject)
                m_OnClosestObjectFound?.Invoke(m_CurrentClosestObject);

            m_LastClosestObject = m_CurrentClosestObject;
        }

        //========================================================

        void Interaction(AreaInteractObject closest)
        {

            if (Input.GetButtonDown("Interact"))
            {
                if (closest)
                {
                    m_LastInteracting = m_CurrentInteracting;
                    m_CurrentInteracting = closest;

                    m_OnInteract?.Invoke(m_CurrentInteracting, m_LastInteracting);

                    if (m_CurrentInteracting.m_StateCanInteract == AreaInteractionState.PickSecond)
                    {
                        ClearCurrentInteracting();
                    }
                }
                else
                {
                    ClearCurrentInteracting();
                }
            }

            if (m_CurrentInteracting)
            {
                m_CurrentState = AreaInteractionState.PickSecond;
            }
            else
            {
                m_CurrentState = AreaInteractionState.PickFirst;
            }

        }

        private void ClearCurrentInteracting()
        {
            m_OnUnInteract?.Invoke(m_CurrentInteracting);
            m_CurrentInteracting = null;
        }

        AreaInteractObject FindColsestObject(AreaInteractionState targetState, AreaInteractObject matchObject = null)
        {
            var count = Physics.OverlapSphereNonAlloc(
            m_DetectCenterP,
            m_DetectRadius,
            m_DetectedObjects,
            m_DetectLayers);

            AreaInteractObject closestObject = null;

            var targetID = string.Empty;

            if (matchObject && matchObject.m_UsingID)
                targetID = matchObject.m_MatchID;

            if (count > 0)
            {
                float distence = Mathf.Infinity;

                for (int i = 0; i < count; i++)
                {
                    var closest = AreaInteractObject.GetObject(m_DetectedObjects[i].transform);
                    var currentDistence = Vector3.Distance(m_DetectCenterP, m_DetectedObjects[i].transform.position);

                    if (!closest ||
                        closest.m_StateCanInteract != targetState ||
                        closest.transform.position.y < m_DetectRangeY ||
                        matchObject &&
                        closest.m_UsingID &&
                        closest.m_MatchID != targetID)
                        continue;

                    if (currentDistence < distence)
                    {
                        distence = currentDistence;
                        closestObject = closest;
                    }
                }
            }

            return closestObject;
        }

        //========================================================

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_DetectCenter.position, m_DetectRadius);

            if (m_CurrentClosestObject)
            {
                var g = Color.green;
                g.a = 0.7f;
                Gizmos.color = g;
                Gizmos.DrawCube(m_CurrentClosestObject.transform.position, Vector3.one);
            }

            if (m_CurrentInteracting)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(m_CurrentInteracting.transform.position, Vector3.one);
            }

            if (m_CurrentClosestObject &&
                m_CurrentClosestObject.m_MatchID != string.Empty &&
                m_CurrentInteracting &&
                m_CurrentInteracting.m_MatchID != string.Empty &&
                m_CurrentClosestObject.m_MatchID == m_CurrentInteracting.m_MatchID)
            {
                var pos1 = m_CurrentClosestObject.transform.position;
                var pos2 = m_CurrentInteracting.transform.position;
                var middlePoint = Vector3.Lerp(pos1, pos2, m_ArrowPosition);
                var perpVector = Vector3.Cross((pos2 - pos1), Vector3.up) / 2 * m_ArrowWidth;


                var right = perpVector + middlePoint;
                var left = -perpVector + middlePoint;

                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(middlePoint, perpVector);
                Gizmos.DrawRay(middlePoint, -perpVector);
                Gizmos.DrawLine(right, pos1);
                Gizmos.DrawLine(left, pos1);
                Gizmos.DrawLine(pos1, pos2);
            }

            {
                Gizmos.color = Color.green;
                var top = transform.position + Vector3.up * m_DetectRadius;
                var bottom = transform.position - Vector3.up * m_DetectRadius;
                var heightRange = Vector3.Lerp(top, bottom, m_DetectionYRange
                            );

                var left = new Vector3(Mathf.Lerp(0, 1, Mathf.PingPong(m_DetectionYRange, 0.5f)), Mathf.Lerp(1, -1, m_DetectionYRange), 0);
                left = left.normalized;
                left.y = Mathf.Lerp(1, -1, m_DetectionYRange);
                left = left * m_DetectRadius + transform.position;

                Gizmos.DrawCube(left, Vector3.one * 0.3f);

                Gizmos.DrawLine(heightRange + Vector3.right * 1.5f, heightRange - Vector3.right * 1.5f);

                Gizmos.DrawLine(top, heightRange);

            }
        }
    }
}