using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class WeaponTest : MonoBehaviour
    {
        public Damagable m_Damagable;
        [SerializeField] WeaponItem[] m_Weapon;
        [SerializeField] bool m_ShowDebug;


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
            SwitchWeapon(1);
        }

        private void Update()
        {
            //攻擊
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_OnAttack?.Invoke(m_Damagable);
            }
            //切換武器
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(1);
            }
            //切換武器
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(2);
            }
            //切換武器
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWeapon(3);
            }
            //傳送位置資訊
            if (m_OnDamagableExist != null)
            {
                m_OnDamagableExist(m_Damagable);
            }
        }

        private void SwitchWeapon(int input)
        {
            input -= 1;
            WeaponItem weapon = null;

            if (input < m_Weapon.Length)
            {
                weapon = m_Weapon[input];
                Debugger.DebugLog($"切換到武器: {weapon.WeaponName}");
                m_OnAttack = weapon.OnAttack;
            }
            else
            {
                Debugger.DebugLog($"切換到近戰");
                m_OnAttack = FistAttack;
            }


            m_OnWeaponSwitch?.Invoke(weapon);

        }

        void FistAttack(Damagable target)
        {
            target.TakeDamage(Random.Range(2, 4));
        }

        bool IsDebugModeOn()
        {
            return m_ShowDebug;
        }
    }
}