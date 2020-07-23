using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISUExample
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager m_Instance;
        Dictionary<string, TextElement> m_Texts = new Dictionary<string, TextElement>();

        private void Awake()
        {
            m_Instance = this;
        }

        public void RegisterText(string key, TextElement text)
        {
            m_Texts.Add(key, text);
        }
        public void UnRegisterText(string key)
        {
            m_Texts.Remove(key);
        }

        public TextElement GetText(string key)
        {
            if (m_Texts.ContainsKey(key))
                return m_Texts[key];
            else
                return null;
        }
    }
}
