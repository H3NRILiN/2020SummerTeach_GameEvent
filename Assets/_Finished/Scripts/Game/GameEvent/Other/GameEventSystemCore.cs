using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ISU.Test.Events
{
    public abstract class GameEventCore<T> : ScriptableObject
    {
        //Listener List

        public void Register(GameEventListenerCore<T> listenerCore)
        {
            Debug.Log(listenerCore.name);
        }

        public void UnRegister(GameEventListenerCore<T> listenerCore)
        {
            Debug.Log(listenerCore.name);
        }

        public void Response(T obj)
        {

        }
    }

    public abstract class GameEventListenerCore<T> : MonoBehaviour
    {
        public abstract GameEventCore<T> m_Event { get; set; }
        public abstract UnityEvent<T> m_Response { get; set; }
    }
}