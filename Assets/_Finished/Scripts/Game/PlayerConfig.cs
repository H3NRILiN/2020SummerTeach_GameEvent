using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Example
{
    [CreateAssetMenu(menuName = "_finished/PlayerConfigurator")]
    public class PlayerConfig : ScriptableObject
    {
        public float m_RotationXSpeed;
        public float m_RotationYSpeed;
        public float m_DampTime;
        public float m_WalkSpeed;
        public float m_JumpHeight;

        public GameEvent m_OnJumpEvent;
    }
}
