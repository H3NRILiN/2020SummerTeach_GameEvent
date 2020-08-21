namespace ISU.Lesson.UNITYEvent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class KeypadNumber : MonoBehaviour
    {
        public int m_Number;
        [SerializeField] Text m_Text;
        [SerializeField] Button m_Button;
        [SerializeField] Image m_Image;
        public Action m_OnKeyPress;

        public void SetText(int number)
        {
            m_Number = number;
            m_Text.text = number.ToString();
        }

        public void SetText(string text)
        {
            m_Text.text = text;
        }

        public void OnClick()
        {
            m_OnKeyPress?.Invoke();
        }
    }
}
