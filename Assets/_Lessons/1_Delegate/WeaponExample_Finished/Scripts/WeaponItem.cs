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
            Debug.Log($"使用武器 : <b>{m_WeaponName}</b>");
            if (!target)
            {
                Debug.Log("<color=yellow>揮空</color>");
                return;
            }
            Debug.Log($"攻擊目標: <b>{target.m_CharacterName}</b>");
            target.TakeDamage(m_Damage);
        }
    }
}