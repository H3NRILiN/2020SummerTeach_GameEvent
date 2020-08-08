namespace ISU.Lesson.Delegate.WeaponExample
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Lessons/1_Delegate/WeaponItem")]
    public class WeaponItem : ScriptableObject
    {
        [SerializeField] string m_WeaponName;
        [SerializeField] float m_Damage;
        public void OnAttack(Damagable target)
        {
            if (!target)
            {
                Debug.Log("揮空");
                return;
            }
            Debug.Log($"攻擊目標: {target.m_CharacterName}");
            target.TakeDamage(m_Damage);
        }
    }
}