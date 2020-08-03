using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// public class VariableCoreEditor<T> : Editor
// {
//     SerializedProperty m_ValueProperty;
//     private void OnEnable()
//     {
//         m_ValueProperty = serializedObject.FindProperty("value");
//     }

//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         GUI.enabled = false;
//         if (serializedObject.FindProperty("staticValue") != null)
//             EditorGUILayout.PropertyField(serializedObject.FindProperty("staticValue"));
//         GUI.enabled = true;
//     }
// }

// [CustomEditor(typeof(ListQuestVariable))]
// public class ListQuestVariableEditor : VariableCoreEditor<List<Quest>>
// {

// }
