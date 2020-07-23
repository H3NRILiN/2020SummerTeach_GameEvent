using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISUExample
{
    [RequireComponent(typeof(Text))]
    public class TextElement : MonoBehaviour
    {
        [SerializeField] string m_KeyWord;
        public Text m_TextUI;
        private void Reset()
        {
            m_TextUI = GetComponent<Text>();
        }
        private void Start()
        {
            UIManager.m_Instance.RegisterText(m_KeyWord, this);
        }
    }
}
