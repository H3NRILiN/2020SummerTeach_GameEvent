using System.Collections;
using System.Collections.Generic;
using Hanzs.Runtime.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.GameEvent
{
    public class HandHoldObjectUITip : MonoBehaviour
    {
        [SerializeField] Text m_Text;
        [SerializeField] string m_HoldObjectTip;
        [SerializeField] string m_DropZoneTip;
        public void OnObjectGet(AreaInteractObject obj)
        {
            if (obj)
            {
                switch (obj._ObjectType)
                {
                    case AreaInteractionType.Holding:
                        SetText(m_HoldObjectTip);
                        break;
                }
            }
            else
            {
                SetText(m_HoldObjectTip);
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