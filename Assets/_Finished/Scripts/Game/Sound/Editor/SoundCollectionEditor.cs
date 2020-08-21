using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace ISU.Sound
{
    [CustomEditor(typeof(SoundCollection))]
    public class SoundCollectionEditor : Editor
    {
        SoundCollection m_Target;
        private void OnEnable()
        {
            m_Target = (SoundCollection)target;
        }
        public override void OnInspectorGUI()
        {
            if (m_Target.m_SoundDatas == null)
                return;

            if (GUILayout.Button("+聲音"))
            {
                UndoRecord();
                m_Target.m_SoundDatas.Add(new SoundData());

            }

            serializedObject.Update();

            for (int i = 0; i < m_Target.m_SoundDatas.Count; i++)
            {
                var soundData = m_Target.m_SoundDatas[i];
                if (soundData == null) continue;
                soundData.name = soundData.soundName;

                GUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("名稱", GUILayout.Width(32));

                        GUILayout.BeginVertical(GUILayout.Width(128));
                        EditorGUI.BeginChangeCheck();
                        soundData.soundName = EditorGUILayout.DelayedTextField(soundData.soundName);
                        if (EditorGUI.EndChangeCheck()) soundData.soundName = Regex.Replace(soundData.soundName, " ", "");
                        if (soundData.soundName == "") GUI.enabled = false;
                        GUILayout.Space(16);
                        soundData.random = GUILayout.Toggle(soundData.random, "隨機撥放");
                        GUILayout.EndVertical();

                        GUILayout.Space(16);
                        EditorGUILayout.PropertyField(
                            serializedObject.FindProperty("m_SoundDatas").GetArrayElementAtIndex(i).FindPropertyRelative("clips")
                            , new GUIContent("音效"));


                        m_Target.m_SoundDatas[i] = soundData;
                        GUI.enabled = true;
                        if (GUILayout.Button("-", GUILayout.Width(32)))
                        {
                            UndoRecord();
                            m_Target.m_SoundDatas.RemoveAt(i);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

            }

            EditorUtility.SetDirty(m_Target);
            serializedObject.ApplyModifiedProperties();


        }

        private void UndoRecord()
        {
            Undo.RecordObject(m_Target, "Sound");
        }
    }
}