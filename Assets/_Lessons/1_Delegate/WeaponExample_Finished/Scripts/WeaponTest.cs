using System;
using UnityEngine;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class WeaponTest : MonoBehaviour
    {
        public Damagable m_Damagable;
        [SerializeField] WeaponItem[] m_Weapon;
        [SerializeField] bool m_ShowDebug;

        int m_CurrentWeaponSlot;

        WeaponItem m_CurrentWeapon { get { return m_Weapon[m_CurrentWeaponSlot]; } }

        public event Action<Damagable> m_OnAttack;
        public event Action<WeaponItem> m_OnWeaponSwitch;
        public event Action<Damagable> m_OnDamagableExist;

        [ContextMenu("清除傷害目標")]
        void ClearDamagableTarget()
        {
            m_Damagable = null;
        }

        private void Awake()
        {
            Debugger.m_DebugLogOn = IsDebugModeOn;
        }
        private void Start()
        {
            SwitchWeapon();
        }

        private void Update()
        {
            //攻擊
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_OnAttack?.Invoke(m_Damagable);
            }
            //切換武器
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeapon();
            }
            //傳送位置資訊
            if (m_OnDamagableExist != null)
            {
                m_OnDamagableExist(m_Damagable);
            }
        }

        private void SwitchWeapon()
        {
            m_CurrentWeaponSlot++;
            m_CurrentWeaponSlot = (int)Mathf.Repeat(m_CurrentWeaponSlot, m_Weapon.Length);

            m_OnWeaponSwitch?.Invoke(m_CurrentWeapon);

            Debugger.DebugLog($"切換到武器: {m_CurrentWeapon.WeaponName}");

            m_OnAttack = m_CurrentWeapon.OnAttack;
        }

        bool IsDebugModeOn()
        {
            return m_ShowDebug;
        }
    }
}