using System;
using System.Collections;
using System.Collections.Generic;
using ISU.Common;
using UnityEngine;
using UnityEngine.UI;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] WeaponTest m_WeaponTest;

        [SerializeField] Image m_IconImg;
        [SerializeField] TextKeywordReplacer m_NameTxt;
        [SerializeField] TextKeywordReplacer m_DamageTxt;
        private void Awake()
        {
            if (!m_WeaponTest)
                m_WeaponTest = FindObjectOfType<WeaponTest>();

            m_WeaponTest.m_OnWeaponSwitch += SetUI;
        }

        void SetUI(WeaponItem item)
        {
            m_IconImg.sprite = item.Icon;
            m_NameTxt.ReplaceText("{name}", item.WeaponName);
            m_DamageTxt.ReplaceText("{damage}", item.Damage.ToString());
        }
    }
}