using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent.Discarded
{
    public enum DetectionMask
    {
        // [InspectorName("無")] None = 0,
        [InspectorName("手拿物")] HandHoldObject = 1,
        [InspectorName("放置位置")] DropZone = 2,
    }
    public class HandHoldInteraction : MonoBehaviour
    {
        [SerializeField] Transform m_HandPosition;
        [SerializeField] float m_DetectRadius;
        [SerializeField] LayerMask m_HitMask;
        [SerializeField] HandHoldObjectEvent m_OnClosestObjectGet;
        [SerializeField] VoidEvent m_OnClosestObjectLost;
        [SerializeField] AudioSource m_AudioSource;
        [SerializeField] AudioClip m_PickupSound;

        Collider[] m_Colliders = new Collider[10];

        Transform m_ClosestObject;
        Transform m_LastClosestObj;

        Action m_OnSelect;
        Action m_OnDeselect;

        Action m_OnClosestFound;
        Action m_OnDetectedEmypty;

        bool m_IsObjectPickup;

        Vector3 m_ClosestObjectFollowPosisionDamp;
        Vector3 m_HandLastPosition;
        private void Update()
        {
            m_HandLastPosition = m_HandPosition.position;

            var mask = !m_IsObjectPickup ? DetectionMask.HandHoldObject : DetectionMask.DropZone;


            m_ClosestObject = GetClosestObject(mask);
            if (m_LastClosestObj != m_ClosestObject)
            {
                if (m_ClosestObject != null)
                {
                    m_OnClosestObjectGet.Raise(HandHoldObject.GetObject(m_ClosestObject.GetInstanceID()));
                }
                else
                {
                    m_OnClosestObjectLost.Raise();
                }
            }
            m_LastClosestObj = m_ClosestObject;


            if (Input.GetButtonDown("Interact"))
            {
                if (!m_IsObjectPickup)
                {
                    if (!m_ClosestObject) return;
                    var interact = m_ClosestObject.GetComponent<HandHoldObject>();
                    if (!interact) return;

                    m_IsObjectPickup = true;
                    m_OnDeselect = interact.OnDrop;

                    interact.OnPickup(m_HandPosition);
                    m_AudioSource.PlayOneShot(m_PickupSound);
                    m_OnClosestObjectLost.Raise();
                }
                else
                {
                    m_OnDeselect();
                    m_IsObjectPickup = false;
                    m_OnDeselect = null;
                    m_LastClosestObj = null;
                }
            }
        }

        private Transform GetClosestObject(DetectionMask mask)
        {
            float closestDistence = Mathf.Infinity;


            Transform closest = null;
            HandHoldObject script = null;
            int count = Physics.OverlapSphereNonAlloc(transform.position, m_DetectRadius, m_Colliders, m_HitMask);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {

                    script = HandHoldObject.GetObject(m_Colliders[i].transform.GetInstanceID());
                    if (!script || script.m_DetectionMask != mask)
                        continue;

                    var curDistence = Vector3.SqrMagnitude(transform.position - m_Colliders[i].transform.position);
                    if (curDistence < closestDistence)
                    {
                        closestDistence = curDistence;
                        closest = m_Colliders[i].transform;
                        continue;
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