using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ISU.Lesson.GameEvent
{
    public abstract class GameEventParameter<T> : ScriptableObject
    {
        List<GameEventListenerParameter<T>> m_Listeners = new List<GameEventListenerParameter<T>>();

        public void Raise(T para)
        {
            for (int i = 0; i < m_Listeners.Count; i++)
            {
                m_Listeners[i].OnEventRaise(para);
            }
        }

        public void RegisterEvent(GameEventListenerParameter<T> listener)
        {
            m_Listeners.Add(listener);
        }

        public void UnRegisterEvent(GameEventListenerParameter<T> listener)
        {
            m_Listeners.Remove(listener);
        }
    }

}