using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSOTester : MonoBehaviour
{
    [SerializeField] TESTSO m_SO;
    [SerializeField, TextArea] string m_InputString;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (m_InputString.Length > 0)
                m_SO.TestStringList.Add(m_InputString);
        }
    }
}
