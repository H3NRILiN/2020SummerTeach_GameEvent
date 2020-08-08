using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIQuestNoticeBlock : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_CanvasGroup;
        [SerializeField] Text m_NameText;
        [SerializeField] Text m_CountText;
        public string m_CurrentQuestIDName;

        public void Notice(Quest quest)
        {
            m_CurrentQuestIDName = quest.IDName;
            UpdateInfo(quest);
            FlyIn();
        }
        public void UpdateInfo(Quest quest)
        {
            if (quest == null)
            {
                m_CanvasGroup.alpha = 0;
                return;
            }
            m_CanvasGroup.alpha = 1;

            m_NameText.text = quest.name;
            m_CountText.text = $"({quest.currentCount}/{quest.goal.goalCount})";
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