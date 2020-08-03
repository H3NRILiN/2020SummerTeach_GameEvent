using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace ISU.Example
{
    [CustomEditor(typeof(GameEventListener))]
    public class GameEventListenerEditor : Editor
    {
        GameEventListener m_Target;

        private void OnEnable()
        {
            m_Target = (GameEventListener)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

        }

    }
}
