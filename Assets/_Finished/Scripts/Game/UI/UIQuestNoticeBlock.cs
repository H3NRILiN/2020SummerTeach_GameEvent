using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIQuestNoticeBlock : MonoBehaviour
    {
        const string m_UncheckBox = "☐";
        const string m_CheckBox = "☑";
        [SerializeField] CanvasGroup m_CanvasGroup;
        [SerializeField] Text m_NameText;
        [SerializeField] Text m_CountText;
        public string m_CurrentQuestIDName;

        public void UpdateUIFirstTime(Quest quest)
        {
            m_CurrentQuestIDName = quest.IDName;
            UpdateUI(quest);
            FlyIn();
        }
        public void UpdateUI(Quest quest)
        {
            m_CanvasGroup.alpha = 1;

            string qChecked = quest.complete ? m_CheckBox : m_UncheckBox;

            m_NameText.text = $"{qChecked} {quest.name}";
            m_CountText.text = $"({quest.currentCount}/{quest.goal.goalCount})";

            if (quest.complete)
            {
                m_NameText.color = Color.green;
                m_CountText.color = Color.green;
            }
        }

        public void ResetUI()
        {
            m_CanvasGroup.alpha = 0;
            m_NameText.color = Color.white;
            m_NameText.color = Color.white;
        }

        private void FlyIn()
        {
            float origninalX = transform.localPosition.x;
            Vector3 pos = transform.localPosition;

            pos.x += ((RectTransform)transform).rect.width;

            transform.localPosition = pos;

            transform.DOLocalMoveX(origninalX, .5f);
        }
    }
}