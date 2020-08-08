using UnityEngine;


namespace ISU.Lesson.Delegate.WeaponExample
{
    public class Damagable : MonoBehaviour
    {
        public string m_CharacterName;
        [SerializeField] float m_Health;
        [ReadOnly, SerializeField] float m_CurrentHealth;

        private void Start()
        {
            m_CurrentHealth = m_Health;
        }

        public void TakeDamage(float amount)
        {
            if (m_CurrentHealth <= 0)
                return;
            m_CurrentHealth -= amount;
            Debug.Log($"<b>{m_CharacterName}</b> 受到<color=orange>{amount}</color>傷害，血量{m_CurrentHealth}/{m_Health}");
            if (m_CurrentHealth <= 0)
            {
                Debug.Log($"<color=red>{m_CharacterName}死亡</color>");
            }
        }
    }
}