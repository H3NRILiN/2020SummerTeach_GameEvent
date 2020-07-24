using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

[CustomEditor(typeof(MultiTag))]
public class MultiTagEditor : Editor
{
    MultiTag m_Target;
    private void OnEnable()
    {
        m_Target = (MultiTag)target;
    }
    public override void OnInspectorGUI()
    {
        string buttonName = "無Tags";
        if (m_Target.tags.Count > 0)
        {
            buttonName = "Tags: ";
            for (int i = 0; i < m_Target.tags.Count; i++)
            {
                buttonName = $"{buttonName}{(i != 0 ? "," : "")}{m_Target.tags[i]}";
            }
        }
        if (GUILayout.Button(buttonName))
        {
            GenericMenuPopup();
        }
    }

    void GenericMenuPopup()
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(new GUIContent("清除Tags"), false, ClearTags);

        menu.AddSeparator("");
        foreach (var tag in InternalEditorUtility.tags)
        {
            menu.AddItem(new GUIContent(tag), m_Target.Compare(tag), () => TagToList(tag));
        }

        menu.ShowAsContext();
    }

    void TagToList(string tag)
    {
        if (m_Target.Compare(tag))
            m_Target.tags.Remove(tag);
        else
            m_Target.tags.Add(tag);
    }

    void ClearTags()
    {
        m_Target.tags = new List<string>();
    }
}
