using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ISU.Lesson.GameEvent
{
    public class GameEventListenerTransform : GameEventListenerParameter<Transform>
    {
        [Serializable]
        public class UnityEventTransform : UnityEvent<Transform>
        {
        }

        [SerializeField] GameEventTransform _event;
        [SerializeField] UnityEventTransform _response;
        public override GameEventParameter<Transform> m_Event { get => _event; }
        public override UnityEvent<Transform> m_Response { get => _response; }

        private void OnEnable()
        {
            m_Event.RegisterEvent(this);
        }

        private void OnDisable()
        {
            m_Event.UnRegisterEvent(this);
        }
    }
}