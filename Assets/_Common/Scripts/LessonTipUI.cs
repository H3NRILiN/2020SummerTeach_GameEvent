using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LessonTipUI : MonoBehaviour
{
    [SerializeField] Text m_TextPrefab;

    [SerializeField] VerticalLayoutGroup m_Layout;

    [SerializeField] [TextArea] string m_Tips;

    private void OnValidate()
    {
        m_TextPrefab.text = m_Tips;
    }
}
