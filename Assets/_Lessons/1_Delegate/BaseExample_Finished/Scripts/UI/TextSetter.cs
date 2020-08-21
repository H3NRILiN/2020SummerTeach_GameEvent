using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class TextSetter : MonoBehaviour
    {
        DelegateTest m_DelegateTest;
        ActionFuncTest m_ActionFuncTest;
        [SerializeField] Text m_Text;

        private void OnEnable()
        {
            m_DelegateTest = FindObjectOfType<DelegateTest>();
            m_DelegateTest.m_OnShowText += SetText;

            m_ActionFuncTest = FindObjectOfType<ActionFuncTest>();
            m_ActionFuncTest.m_OnShowText += SetText;
        }
        private void OnDisable()
        {
            m_DelegateTest.m_OnShowText -= SetText;

            m_ActionFuncTest.m_OnShowText -= SetText;

        }
        public void SetText(string text, Color color, Vector2 scale)
        {
            m_Text.text = text;
            m_Text.color = color;
            m_Text.rectTransform.localScale = scale;
        }
    }
}