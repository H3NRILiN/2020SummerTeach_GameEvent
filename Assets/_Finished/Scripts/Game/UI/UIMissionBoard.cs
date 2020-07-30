using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ISU.Example
{
    public class UIMissionBoard : MonoBehaviour
    {
        [SerializeField] Text m_Title;
        [SerializeField] Text m_Description;
        [SerializeField] Button m_AcceptButton;
        [SerializeField] CanvasGroup m_CanvasGroup;

        public void SetInfos(Quest quest, UnityAction acceptAction)
        {
            m_Title.text = quest.name;
            m_Description.text = quest.description;
            m_AcceptButton.onClick.AddListener(acceptAction);

            if (quest.active)
            {
                OnQuestActive();
            }
        }

        public void OnQuestActive()
        {
            m_CanvasGroup.alpha = 0.5f;
            m_CanvasGroup.interactable = false;
        }
    }
}