﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ISU.Common
{
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        Interactable m_Target;
        private void OnEnable()
        {
            m_Target = (Interactable)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (m_Target.m_SubInteractor == null)
                m_Target.m_SubInteractor = m_Target.GetComponent<SubInteractor>();
        }
    }
}