using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIInteractionPanel : MonoBehaviour
    {
        [SerializeField] GameObject m_Panel;
        [SerializeField] Text m_Text;
        [SerializeField] UIProgress m_KeyPressProgress;

        public void Show(string content, Color color, bool keyPress, string key = "")
        {
            m_Panel.SetActive(true);
            m_Text.text = content;
            m_Text.color = color;

            if (keyPress)
            {
                m_KeyPressProgress.Show(key);
            }
            else
            {
                m_KeyPressProgress.Hide();
            }
        }

        public void Hide()
        {
            m_Panel.SetActive(false);
            m_KeyPressProgress.Hide();
        }
    }
}