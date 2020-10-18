using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    public class AreaInteraction : MonoBehaviour
    {

        [SerializeField] float m_DetectRadius;
        [SerializeField] LayerMask m_HitMask;

        [SerializeField] GameEventTransform m_OnGetClosestObject;
        Collider[] m_Colliders = new Collider[99];

        [SerializeField] Transform closestOBJ;

        Transform lastClosestObj;
        private void Update()
        {
            Vector3 closestObject = transform.up * 999;
            Transform closest = null;

            int count = Physics.OverlapSphereNonAlloc(transform.position, m_DetectRadius, m_Colliders, m_HitMask);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (Vector3.SqrMagnitude(transform.position - m_Colliders[i].transform.position)
                    < Vector3.SqrMagnitude(transform.position - closestObject))
                    {
                        closestObject = m_Colliders[i].transform.position;
                        closest = m_Colliders[i].transform;
                    }
                }
            }
            closestOBJ = closest;

            if (lastClosestObj != closestOBJ)
            {

                m_OnGetClosestObject.Raise(closestOBJ);
            }

            lastClosestObj = closestOBJ;

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, m_DetectRadius);
        }
    }
}