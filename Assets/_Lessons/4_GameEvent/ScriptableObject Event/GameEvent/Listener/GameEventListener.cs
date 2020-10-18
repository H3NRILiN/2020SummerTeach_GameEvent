using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//來自：https://unity.com/how-to/architect-game-code-scriptable-objects
namespace ISU.Lesson.GameEvent
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent m_Event;
        [SerializeField] UnityEvent m_Response;

        private void OnEnable()
        {
            m_Event.RegisterEvent(this);
        }

        private void OnDisable()
        {
            m_Event.UnRegisterEvent(this);
        }

        public void OnEventRaise()
        {
            m_Response.Invoke();
        }
    }
}
