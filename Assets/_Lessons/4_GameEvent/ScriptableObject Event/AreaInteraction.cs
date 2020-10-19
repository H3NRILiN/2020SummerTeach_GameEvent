using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    public class AreaInteraction : MonoBehaviour
    {
        [SerializeField] Transform m_HandPosition;
        [SerializeField] float m_DetectRadius;
        [SerializeField] LayerMask m_HitMask;
        [SerializeField] TransformEvent m_OnClosestObjectGet;
        [SerializeField] VoidEvent m_OnClosestObjectLost;
        [SerializeField] AudioSource m_AudioSource;
        [SerializeField] AudioClip m_PickupSound;

        Collider[] m_Colliders = new Collider[99];

        Transform m_ClosestObject;
        Transform m_LastClosestObj;

        Action<Vector3> m_OnObjectDrop;

        bool m_IsObjectPickup;

        Vector3 m_ClosestObjectFollowPosisionDamp;
        Vector3 m_HandLastPosition;
        private void Update()
        {
            m_HandLastPosition = m_HandPosition.position;


            if (!m_IsObjectPickup)
            {
                m_ClosestObject = GetClosestObject();

                if (m_LastClosestObj != m_ClosestObject)
                {
                    if (m_ClosestObject != null)
                    {
                        m_OnClosestObjectGet.Raise(m_ClosestObject);
                    }
                    else
                    {
                        m_OnClosestObjectLost.Raise();
                    }
                }

                m_LastClosestObj = m_ClosestObject;
            }




            if (Input.GetButtonDown("Interact"))
            {
                if (!m_IsObjectPickup)
                {
                    if (!m_ClosestObject) return;
                    var interact = m_ClosestObject.GetComponent<AreaInteractionObject>();
                    if (!interact) return;
                    interact.OnPickup(m_HandPosition);

                    m_OnObjectDrop = interact.OnDrop;

                    m_IsObjectPickup = true;

                    m_AudioSource.PlayOneShot(m_PickupSound);

                    m_OnClosestObjectLost.Raise();
                }
                else
                {
                    m_OnObjectDrop(m_HandPosition.position - m_HandLastPosition);

                    m_OnObjectDrop = null;

                    m_IsObjectPickup = false;

                    m_LastClosestObj = null;


                }
            }
        }

        private Transform GetClosestObject()
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

            return closest;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, m_DetectRadius);
        }
    }
}