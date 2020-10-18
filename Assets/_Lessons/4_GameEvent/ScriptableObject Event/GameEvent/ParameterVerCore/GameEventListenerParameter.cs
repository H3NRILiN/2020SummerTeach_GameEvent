using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ISU.Lesson.GameEvent
{
    public abstract class GameEventListenerParameter<T> : MonoBehaviour
    {
        abstract public GameEventParameter<T> m_Event { get; }
        abstract public UnityEvent<T> m_Response { get; }


        public void OnEventRaise(T para)
        {
            m_Response.Invoke(para);
        }
    }


}
