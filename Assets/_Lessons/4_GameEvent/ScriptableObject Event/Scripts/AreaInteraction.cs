using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Rendering;
#endif

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

        [Header("Debug")]
        [SerializeField] bool m_DebugMode;

        //========================================================

        Vector3 m_DetectCenterP { get { return m_DetectCenter.position; } }
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
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (!m_DebugMode)
                return;
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(m_DetectCenter.position, m_DetectRadius);

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

            if (m_CurrentClosestObject &&
                m_CurrentClosestObject.m_MatchID != string.Empty &&
                m_CurrentInteracting &&
                m_CurrentInteracting.m_MatchID != string.Empty &&
                m_CurrentClosestObject.m_MatchID == m_CurrentInteracting.m_MatchID)
            {
                Color arrowColor = Color.cyan;
                arrowColor.a = 0.5f;
                Handles.color = arrowColor;

                var pos1 = m_CurrentClosestObject.transform.position;
                var pos2 = m_CurrentInteracting.transform.position;
                var vector = pos1 - pos2;

                //畫鍵頭
                var arrowAngle = Quaternion.FromToRotation(Vector3.forward, pos1 - pos2); //從forward轉向向量的rotation
                var arrowGap = 0.7f; //間隔
                var ignoreLast = 3; //忽略最後幾個
                var count = vector.magnitude / arrowGap;
                for (int i = 0; i < count; i++)
                {
                    if (count - i > ignoreLast)
                        Handles.ArrowHandleCap(0, vector * (i / count) + pos2, arrowAngle, 1.5f, EventType.Repaint);

                }
                Handles.DrawLine(pos1, pos2);
            }

            {
                var rediusOffset = 0;

                Gizmos.color = Color.green;
                var top = transform.position + Vector3.up * m_DetectRadius;
                var bottom = transform.position - Vector3.up * m_DetectRadius;
                var height = new Vector3(transform.position.x, m_DetectRangeY, transform.position.z);



                var radius =
                Mathf.Cos
                (Mathf.Lerp(m_DetectionRangeAngleMinMax.x, m_DetectionRangeAngleMinMax.y, m_DetectionYRange) * Mathf.Deg2Rad)
                * (m_DetectRadius + rediusOffset);


                var angle = Mathf.Lerp(m_DetectionRangeAngleMinMax.x, m_DetectionRangeAngleMinMax.y, m_DetectionYRange);

                Handles.color = new Color(0, 1, 0, 1);
                DrawDetectRangeHandles(CompareFunction.LessEqual, top, height, radius, angle);
                Handles.color = new Color(0, 1, 0, 0.2f);
                DrawDetectRangeHandles(CompareFunction.Greater, top, height, radius, angle);

                // Debug.Log(m_DetectRangeY);
            }
        }
        //繪製自己的球體，會根據Y範圍被切開，標示偵測範圍
        private void DrawDetectRangeHandles(CompareFunction zTest, Vector3 top, Vector3 height, float radius, float angle)
        {
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
        }
#endif
    }
}