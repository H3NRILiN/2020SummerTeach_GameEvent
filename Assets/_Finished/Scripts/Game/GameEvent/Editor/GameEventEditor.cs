using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ISU.Example
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
                GUI.enabled = false;
            if (GUILayout.Button("Test Raise"))
            {
                ((GameEvent)target).Raise();
            }
        }
    }
}