using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//來自：https://unity.com/how-to/architect-game-code-scriptable-objects
namespace ISU.Lesson.GameEvent
{
    [CreateAssetMenu(menuName = "Lessons/GameEvent/Event")]
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> m_Listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = 0; i < m_Listeners.Count; i++)
            {
                m_Listeners[i].OnEventRaise();
            }
        }

        public void RegisterEvent(GameEventListener listener)
        {
            m_Listeners.Add(listener);
        }

        public void UnRegisterEvent(GameEventListener listener)
        {
            m_Listeners.Remove(listener);
        }
    }
}