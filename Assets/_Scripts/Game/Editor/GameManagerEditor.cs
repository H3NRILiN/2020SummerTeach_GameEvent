using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ISU.Common
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        GameManager m_Target;
        private void OnEnable()
        {
            m_Target = (GameManager)target;
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var guiEnable = GUI.enabled;

            if (!Application.isPlaying)
                GUI.enabled = false;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("開啟游標"))
            {
                m_Target.FPSCursorLock(false);
            }
            if (GUILayout.Button("關閉游標"))
            {
                m_Target.FPSCursorLock(true);
            }
            GUILayout.EndHorizontal();

            GUI.enabled = guiEnable;
        }
    }
}