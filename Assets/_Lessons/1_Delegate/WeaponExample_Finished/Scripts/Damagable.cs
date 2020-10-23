using UnityEngine;
using DG.Tweening;
using System;

namespace ISU.Lesson.Delegate.WeaponExample
{
    public class Damagable : MonoBehaviour
    {
        public string m_CharacterName;
        [SerializeField] float m_Health;
        [SerializeField] Transform m_DisplayObject;
        [ReadOnly, SerializeField] float m_CurrentHealth;

        Vector3 m_OriginalPostion;

        Sequence m_CurrentSequence;

        public static Action<Damagable, float> m_OnTakeDamage;
        private void Start()
        {
            m_CurrentHealth = m_Health;

            m_OriginalPostion = m_DisplayObject.position;

            m_CurrentSequence = DOTween.Sequence();
        }

        public void TakeDamage(float amount)
        {
            if (m_CurrentHealth <= 0)
                return;
            m_OnTakeDamage?.Invoke(this, amount);
            m_CurrentHealth -= amount;

            m_CurrentSequence
             .Append(m_DisplayObject.DOShakePosition(0.2f, 0.3f, 20, 100, false, true))
             .Append(m_DisplayObject.DOMove(m_OriginalPostion, 0.1f));


            Debugger.DebugLog($"<b>{m_CharacterName}</b> 受到<color=orange>{amount}</color>傷害，血量{m_CurrentHealth}/{m_Health}");
            if (m_CurrentHealth <= 0)
            {
                Debugger.DebugLog($"<color=red>{m_CharacterName}死亡</color>");
                gameObject.SetActive(false);
            }
        }
    }
}