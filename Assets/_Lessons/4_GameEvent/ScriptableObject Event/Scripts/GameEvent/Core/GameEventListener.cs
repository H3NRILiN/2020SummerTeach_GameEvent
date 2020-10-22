using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//來自：https://www.youtube.com/watch?v=iXNwWpG7EhM&ab_channel=DapperDino
namespace ISU.Lesson.GameEvent
{
    /// <summary>
    /// 事件接收器
    /// </summary>
    /// <typeparam name="T">參數類型</typeparam>
    /// <typeparam name="E">T類型的GameEvent</typeparam>
    /// <typeparam name="UE">T類型的UnityEvent</typeparam>
    public class GameEventListener<T, E, UE> : MonoBehaviour, IEventListener<T>
    where E : GameEvent<T>
    where UE : UnityEvent<T>
    {
        [SerializeField] E m_Event;
        [SerializeField] UE m_Response;

        private void OnEnable()
        {
            m_Event.RegisterEvent(this);
        }

        private void OnDisable()
        {
            m_Event.UnRegisterEvent(this);
        }

        public void OnEventRaise(T parameter)
        {
            m_Response.Invoke(parameter);
        }
    }
}
