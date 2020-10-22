using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.GameEvent
{
    public class HandHoldObjectUITip : MonoBehaviour
    {
        [SerializeField] Text m_Text;
        [SerializeField] string m_HoldObjectTip;
        [SerializeField] string m_DropZoneTip;
        public void OnObjectGet(HandHoldObject obj)
        {
            switch (obj.m_DetectionMask)
            {
                case DetectionMask.HandHoldObject:
                    SetText(m_HoldObjectTip);
                    break;
                case DetectionMask.DropZone:
                    SetText(m_DropZoneTip);
                    break;
            }
        }

        void SetText(string text)
        {
            if (m_Text.text != text)
            {
                m_Text.text = text;
            }
        }
    }
}