namespace ISU.Lesson.Delegate.WeaponExample
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Lessons/1_Delegate/WeaponItem")]
    public class WeaponItem : ScriptableObject
    {
        [SerializeField] string m_WeaponName;
        [SerializeField] float m_Damage;
        [SerializeField] Sprite m_Icon;



        public string WeaponName { get => m_WeaponName; }
        public float Damage { get => m_Damage; }
        public Sprite Icon { get => m_Icon; }

        public void OnAttack(Damagable target)
        {

            Debugger.DebugLog($"使用武器 : <b>{WeaponName}</b>");
            if (!target)
            {
                Debugger.DebugLog("<color=yellow>揮空</color>");
                return;
            }
            Debugger.DebugLog($"攻擊目標: <b>{target.m_CharacterName}</b>");
            target.TakeDamage(Damage);
        }


    }
}