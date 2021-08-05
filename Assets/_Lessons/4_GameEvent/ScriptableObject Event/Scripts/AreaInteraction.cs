using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Rendering;
#endif

namespace Hanzs.Runtime.Interaction
{
    public enum AreaInteractionState
    {
        [InspectorName("只能被獨立選擇")] NoInteractiveObject,
        [InspectorName("需要物件組合")] Interacting
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
        [SerializeField, Range(0, 1)] float m_DetectAngleCheckRadius;
        [SerializeField, Range(0, 1)] float m_DetectionYRange;
        [SerializeField, Range(0, 180)] float m_DetectionForwordAngle;
        [SerializeField] LayerMask m_DetectLayers = 9;
        //========================================================

        [Header("Debug")]
        [SerializeField] bool m_DebugMode;
        [SerializeField] bool m_ShowDetectRanges;
        [SerializeField] bool m_ShowObjectsPositionInfo;
        [SerializeField] bool m_ShowCombinationArrow;
        [SerializeField] bool m_ShowAllDetectedObject;

        //========================================================

        Vector3 m_DetectCenterPos { get { return m_DetectCenter.position; } }
        Vector2 m_DetectionRangeAngleMinMax = new Vector2(90, -90);
        AreaInteractionState m_CurrentState;
        AreaInteractObject m_CurrentClosestObject;
        AreaInteractObject m_LastClosestObject;
        AreaInteractObject m_CurrentInteracting;
        AreaInteractObject m_LastInteracting;
        Collider[] m_DetectedObjects = new Collider[10];
        float m_DetectRangeYLerpT { get => Mathf.Sin(Mathf.Lerp(m_DetectionRangeAngleMinMax.x, m_DetectionRangeAngleMinMax.y, m_DetectionYRange) * Mathf.Deg2Rad); }
        float m_DetectRangeY { get => transform.position.y + m_DetectRangeYLerpT * m_DetectRadius; }

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

        private void Start()
        {
            if (!m_DetectCenter) m_DetectCenter = this.transform;
            m_CurrentState = AreaInteractionState.NoInteractiveObject;
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

                    //組合狀態，取消組合物的互動狀態
                    if (m_LastInteracting) m_OnUnInteract?.Invoke(m_LastInteracting);

                    //觸發當前物件的互動
                    m_OnInteract?.Invoke(m_CurrentInteracting, m_LastInteracting);

                    //直接取消當前互動物互動狀態
                    if (m_CurrentInteracting._InteractionRequirment == AreaInteractionState.Interacting)
                    {
                        ClearCurrentInteracting();
                    }
                }
                else
                {
                    if (m_CurrentState == AreaInteractionState.Interacting)
                    {
                        ClearCurrentInteracting();
                    }
                }
            }

            if (m_CurrentInteracting)
            {
                m_CurrentState = AreaInteractionState.Interacting;
            }
            else
            {
                m_CurrentState = AreaInteractionState.NoInteractiveObject;
            }


        }

        private void ClearCurrentInteracting()
        {
            m_OnUnInteract?.Invoke(m_CurrentInteracting);
            m_CurrentInteracting = null;
        }

        private void ClearLastInteracting()
        {
            m_OnUnInteract?.Invoke(m_LastInteracting);
            //m_LastInteracting = null;
        }

        AreaInteractObject FindColsestObject(AreaInteractionState targetState, AreaInteractObject matchObject = null)
        {
            var count = Physics.OverlapSphereNonAlloc(
            m_DetectCenterPos,
            m_DetectRadius,
            m_DetectedObjects,
            m_DetectLayers);

            AreaInteractObject finalClosest = null;
            float finalDistence = Mathf.Infinity;
            float finalAngle = Mathf.Infinity;



            var targetID = string.Empty;

            if (matchObject && matchObject._RequiredMatchObject)
                targetID = matchObject._MatchID;

            AreaInteractObject lastIgnored = null;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var curClosest = AreaInteractObject.GetObject(m_DetectedObjects[i].transform);

                    if (!curClosest ||
                        !curClosest._IsActive ||
                        curClosest._InteractionRequirment != targetState ||
                        curClosest.transform.position.y < m_DetectRangeY)
                    {
                        lastIgnored = curClosest;
                        continue;
                    }
                    //========================================================

                    RaycastHit hit;

                    var obstacleDetected = Physics.Raycast(m_DetectCenterPos,
                             curClosest.transform.position - m_DetectCenterPos,
                             out hit,
                             (curClosest.transform.position - m_DetectCenterPos).magnitude, ~(m_DetectLayers));
                    if (obstacleDetected)
                    {
                        if (lastIgnored)
                        {
                            if (hit.transform.GetInstanceID() != lastIgnored.transform.GetInstanceID() &&
                                hit.transform.GetInstanceID() != lastIgnored._ParentObject.GetInstanceID()
                                )
                            {
                                if (hit.transform.GetInstanceID() != curClosest.transform.GetInstanceID() &&
                                    hit.transform.GetInstanceID() != curClosest._ParentObject.GetInstanceID()
                                    )
                                    continue;
                            }
                        }
                    }

                    //========================================================
                    var curCenterToClosestYAngle = Mathf.Abs(Vector3.SignedAngle(curClosest.transform.position - transform.position, transform.forward, Vector3.up));

                    if (curCenterToClosestYAngle > m_DetectionForwordAngle)
                        continue;
                    //========================================================
                    if (matchObject &&
                        curClosest._RequiredMatchObject &&
                        curClosest._MatchID != targetID)
                        continue;
                    //========================================================

                    //========================================================

                    var curDistance = Vector3.Distance(m_DetectCenterPos, m_DetectedObjects[i].transform.position);

                    bool distenceMatch = curDistance < finalDistence;
                    bool angleMatch = curCenterToClosestYAngle < finalAngle;

                    if (angleMatch && distenceMatch)
                    {
                        finalDistence = curDistance;
                        finalClosest = curClosest;
                        finalAngle = curCenterToClosestYAngle;
                    }
                }
            }

            return finalClosest;
        }

        //========================================================
#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            if (!m_DebugMode)
                return;
            if (!m_DetectCenter)
                return;
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(m_DetectCenter.position, m_DetectRadius);

            if (m_ShowObjectsPositionInfo)
            {
                if (m_CurrentClosestObject)
                {
                    var g = Color.green;
                    g.a = 0.7f;
                    Gizmos.color = g;
                    //Gizmos.DrawCube(m_CurrentClosestObject.transform.position, Vector3.one);
                    var trans = m_CurrentClosestObject.transform;
                    var meshFilter = m_CurrentClosestObject.GetComponentInParent<MeshFilter>();
                    if (!meshFilter) meshFilter = m_CurrentClosestObject.GetComponentInChildren<MeshFilter>();
                    Mesh mesh = null;
                    if (meshFilter)
                    {
                        mesh = meshFilter.sharedMesh;
                    }
                    var scale = 1.1f;

                    //測試:若Anchor中心點不在物件中央，就加上offset
                    var scaleOffset = (Mathf.Abs(1 - scale)) / 2;
                    if (mesh)
                        Gizmos.DrawMesh(mesh, trans.position - trans.TransformDirection(trans.localPosition + (trans.localPosition == Vector3.zero ? Vector3.zero : Vector3.one * scaleOffset)), trans.rotation, Vector3.one * scale);
                }

                if (m_CurrentInteracting)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(m_CurrentInteracting.transform.position, Vector3.one);
                }
            }

            if (m_ShowCombinationArrow)
            {
                if (m_CurrentClosestObject &&
                m_CurrentClosestObject._MatchID != string.Empty &&
                m_CurrentInteracting &&
                m_CurrentInteracting._MatchID != string.Empty &&
                m_CurrentClosestObject._MatchID == m_CurrentInteracting._MatchID)
                {
                    Color arrowColor = Color.cyan;
                    arrowColor.a = 0.5f;
                    Handles.color = arrowColor;

                    var pos1 = m_CurrentClosestObject.transform.position;
                    var pos2 = m_CurrentInteracting.transform.position;
                    var vector = pos1 - pos2;

                    //畫鍵頭
                    var arrowAngle = Quaternion.FromToRotation(Vector3.forward, pos1 - pos2); //從forward轉向向量的rotation
                    var arrowGap = 0.5f; //間隔
                    var ignoreLast = 0; //忽略最後幾個

                    var count = vector.magnitude / arrowGap;
                    for (int i = 0; i < count; i++)
                    {
                        if (count - i > ignoreLast)
                            Handles.ConeHandleCap(0, vector * (i / count) + pos2, arrowAngle, 0.3f, EventType.Repaint);

                    }
                    Handles.DrawLine(pos1, pos2);
                }
            }

            if (m_ShowDetectRanges)
            {
                var radiusOffset = 0;

                Gizmos.color = Color.green;
                var top = m_DetectCenter.position + Vector3.up * m_DetectRadius;
                var bottom = m_DetectCenter.position - Vector3.up * m_DetectRadius;
                var height = new Vector3(m_DetectCenter.position.x, m_DetectRangeY, m_DetectCenter.position.z);



                var radius =
                Mathf.Cos
                (Mathf.Lerp(m_DetectionRangeAngleMinMax.x, m_DetectionRangeAngleMinMax.y, m_DetectionYRange) * Mathf.Deg2Rad)
                * (m_DetectRadius + radiusOffset);


                var angle = Mathf.Lerp(m_DetectionRangeAngleMinMax.x, m_DetectionRangeAngleMinMax.y, m_DetectionYRange);

                Handles.color = new Color(0, 1, 0, 1);
                DrawDetectRangeHandles(CompareFunction.LessEqual, top, height, radius, angle);
                Handles.color = new Color(0, 1, 0, 0.2f);
                DrawDetectRangeHandles(CompareFunction.Greater, top, height, radius, angle);

                var fowordAngleCenter = height.y >= m_DetectCenter.position.y ? height : m_DetectCenter.position;
                var forowrdAngleRadius = height.y >= m_DetectCenter.position.y ? radius : m_DetectRadius;
                Handles.color = new Color(0, .7f, 0, 0.15f);
                DrawForowrdAngleHandles(CompareFunction.Disabled, fowordAngleCenter, forowrdAngleRadius);

                var angleCheckRadius = Mathf.Lerp(0, m_DetectRadius, m_DetectAngleCheckRadius);

                Handles.color = new Color(0, .7f, 7f, 0.2f);
                DrawForowrdAngleHandles(CompareFunction.Disabled, fowordAngleCenter, angleCheckRadius);

                // Debug.Log(m_DetectRangeY);
            }

            if (m_ShowAllDetectedObject)
            {
                Handles.color = Color.magenta;
                if (m_DetectedObjects.Length > 0)
                {
                    foreach (var obj in m_DetectedObjects)
                    {
                        if (
                        obj &&
                        (obj.transform.position - m_DetectCenter.position).magnitude <= m_DetectRadius &&
                        obj.transform.position.y >= m_DetectRangeY
                        )
                        {
                            var z = Handles.zTest;
                            Handles.zTest = CompareFunction.Always;
                            Handles.DrawLine(m_DetectCenter.position, obj.transform.position);

                            Handles.zTest = z;
                        }
                    }
                }
            }

        }
        //繪製自己的球體，會根據Y範圍被切開，標示偵測範圍
        private void DrawDetectRangeHandles(CompareFunction zTest, Vector3 top, Vector3 height, float radius, float angle)
        {
            var ztest = Handles.zTest;
            Handles.zTest = zTest;

            var correctionAngle = angle - 90;
            Vector3[] sides = new Vector3[] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            foreach (var side in sides)
            {
                Handles.DrawWireArc(transform.position, side, Vector3.up, correctionAngle, m_DetectRadius);
            }
            Handles.DrawWireDisc(height, Vector3.up, radius);
            if (height.y <= transform.position.y) Handles.DrawWireDisc(transform.position, Vector3.up, m_DetectRadius);
            Handles.DrawLine(height + Vector3.right * radius, height - Vector3.right * radius);
            Handles.DrawLine(height + Vector3.forward * radius, height - Vector3.forward * radius);
            Handles.DrawLine(top, height);



            Handles.zTest = ztest;
        }

        void DrawForowrdAngleHandles(CompareFunction zTest, Vector3 position, float radius)
        {
            var ztest = Handles.zTest;
            Handles.zTest = zTest;
            Handles.DrawSolidArc(position, Vector3.up, m_DetectCenter.forward, m_DetectionForwordAngle, radius);
            Handles.DrawSolidArc(position, Vector3.up, m_DetectCenter.forward, -m_DetectionForwordAngle, radius);
            Handles.zTest = ztest;
        }
#endif
    }
}