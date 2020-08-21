namespace ISU.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class GameState
    {
        public static Action<bool> m_PlayerCanMove;
        public static Action<bool> m_PlayerCursorLock;
        public static Action<bool> m_CameraAniamtionFollowing;
    }
}