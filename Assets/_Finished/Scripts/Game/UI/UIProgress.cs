using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIProgress : MonoBehaviour
    {
        [SerializeField] GameObject m_Panel;
        [SerializeField] Text m_Text;
        [SerializeField] Image m_ProgressImage;

        public void Show(string text, float amount = 0)
        {
            m_ProgressImage.fillAmount = amount;
            m_Text.text = text;
            m_Panel.SetActive(true);
        }

        public void SetProgress(float amount)
        {
            m_ProgressImage.fillAmount = amount;
        }
        public void Hide()
        {
            m_Panel.SetActive(false);
        }
    }
}