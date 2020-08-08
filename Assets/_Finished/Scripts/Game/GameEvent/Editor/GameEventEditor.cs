using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace ISU.Example.Events
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        GameEvent m_Target;
        private void OnEnable()
        {
            m_Target = (GameEvent)target;
        }
        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
                GUI.enabled = false;
            if (GUILayout.Button("Test Raise"))
            {
                m_Target.Raise();
            }

            foreach (var gl in Resources.FindObjectsOfTypeAll<GameEventListener>())
            {
                if (gl.m_Event == m_Target)
                {
                    if (gl.gameObject.scene == EditorSceneManager.GetActiveScene())
                    {
                        GUILayout.Space(5);

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("註冊的Listener : ", GUILayout.Width(96));
                        EditorGUILayout.ObjectField(gl, typeof(GameEventListener), allowSceneObjects: true);
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}