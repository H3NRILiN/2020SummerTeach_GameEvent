using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVariable : MonoBehaviour
{
    [SerializeField] ListQuestVariable m_Q;
    private void Update()
    {
        Debug.Log(m_Q.value.Count);
        foreach (var item in m_Q.value)
        {
            Debug.Log(item.name);
        }
    }
}
