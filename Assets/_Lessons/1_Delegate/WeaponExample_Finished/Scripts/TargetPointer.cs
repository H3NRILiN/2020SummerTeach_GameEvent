using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ISU.Common;
using System;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class TargetPointer : MonoBehaviour
    {
        [SerializeField] WeaponTest m_WeaponTest;

        [SerializeField] GameObject m_Pointer;
        [SerializeField] Vector3 m_WorldSpaceOffset;
        Camera m_MainCam;
        void Awake()
        {
            m_MainCam = FindObjectOfType<Camera>();
            if (!m_WeaponTest)
                m_WeaponTest = FindObjectOfType<WeaponTest>();

            m_WeaponTest.m_OnDamagableExist += SetPointerPosition;
        }

        private void SetPointerPosition(Damagable obj)
        {
            if (obj)
            {
                m_Pointer.SetActive(true);
                m_Pointer.transform.position = m_MainCam.WorldToScreenPoint(obj.transform.position + m_WorldSpaceOffset);
            }
            else
            {
                m_Pointer.SetActive(false);
            }
        }

        void Update()
        {

        }
    }
}