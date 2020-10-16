using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartupCommentWindow : EditorWindow
{
    private void OnGUI()
    {

    }

    private void OnLostFocus()
    {
        Close();
    }
}
