using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//來自：https://www.youtube.com/watch?v=iXNwWpG7EhM&ab_channel=DapperDino
namespace ISU.Lesson.GameEvent
{
    public abstract class GameEvent<T> : ScriptableObject
    {

        List<IEventListener<T>> m_Listeners = new List<IEventListener<T>>();

        public void Raise(T parameter)
        {
            for (int i = 0; i < m_Listeners.Count; i++)
            {
                m_Listeners[i].OnEventRaise(parameter);
            }
        }

        public void RegisterEvent(IEventListener<T> listener)
        {
            if (!m_Listeners.Contains(listener)) m_Listeners.Add(listener);
        }

        public void UnRegisterEvent(IEventListener<T> listener)
        {
            if (m_Listeners.Contains(listener)) m_Listeners.Remove(listener);
        }
    }
}