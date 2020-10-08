using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Common
{
    [RequireComponent(typeof(Text))]
    public class TextKeywordReplacer : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField, TextArea] string m_ReplaceRules;
        Text m_text;
        private void Awake()
        {
            m_text = GetComponent<Text>();
        }

        public void ReplaceText(string key, string result)
        {
            m_text.text = m_ReplaceRules.Replace(key, result);
        }

        public void OnBeforeSerialize()
        {
            if (!m_text)
                m_text = GetComponent<Text>();
            m_text.text = m_ReplaceRules;
        }

        public void OnAfterDeserialize()
        {


        }
    }
}