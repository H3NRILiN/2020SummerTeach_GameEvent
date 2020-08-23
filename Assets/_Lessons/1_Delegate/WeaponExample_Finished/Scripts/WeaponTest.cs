using System;
using UnityEngine;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] Damagable m_Damagable;
        [SerializeField] WeaponItem[] m_Weapon;

        int m_CurrentWeaponSlot;

        Action<Damagable> m_OnAttack;

        private void Start()
        {
            SwitchWeapon();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_OnAttack?.Invoke(m_Damagable);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeapon();
            }
        }

        private void SwitchWeapon()
        {
            m_CurrentWeaponSlot++;
            m_CurrentWeaponSlot = (int)Mathf.Repeat(m_CurrentWeaponSlot, m_Weapon.Length);

            Debug.Log($"切換到武器: {m_Weapon[m_CurrentWeaponSlot].GetName()}");

            m_OnAttack = m_Weapon[m_CurrentWeaponSlot].OnAttack;
        }
    }
}