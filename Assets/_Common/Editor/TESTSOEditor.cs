using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TESTSO))]
public class TESTSOEditor : Editor
{
    TESTSO m_Target;
    private void OnEnable()
    {
        m_Target = (TESTSO)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("+"))
        {
            m_Target.TestStringList.Add("");
        }
        for (int i = 0; i < m_Target.TestStringList.Count; i++)
        {
            m_Target.TestStringList[i] = GUILayout.TextArea(m_Target.TestStringList[i]);

        }

        EditorUtility.SetDirty(m_Target);
    }
}
