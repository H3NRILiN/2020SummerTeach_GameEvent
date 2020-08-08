using UnityEngine;


namespace ISU.Lesson.Delegate.WeaponExample
{
    public class Damagable : MonoBehaviour
    {
        public string m_CharacterName;
        public float m_Health;

        public void TakeDamage(float amount)
        {
            if (m_Health <= 0)
                return;
            m_Health -= amount;
            Debug.Log($"{m_CharacterName}受到{amount}傷害，剩餘血量{m_Health}");
            if (m_Health <= 0)
            {
                Debug.Log($"{m_CharacterName}死亡");
            }
        }
    }
}