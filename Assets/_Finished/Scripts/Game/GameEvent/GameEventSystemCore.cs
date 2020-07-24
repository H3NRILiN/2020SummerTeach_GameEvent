using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class GameEventCore : ScriptableObject
{
    List<GameEventListenerCore> m_Listeners = new List<GameEventListenerCore>();

    public void Register(GameEventListenerCore listener)
    {
        m_Listeners.Add(listener);
    }

    public void UnRegister(GameEventListenerCore listener)
    {
        m_Listeners.Remove(listener);
    }

    public void DoInvoke()
    {
        for (int i = 0; i < m_Listeners.Count; i++)
        {
            m_Listeners[i].m_Response.Invoke();
        }
    }
}

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

public abstract class GameEventListenerCore : MonoBehaviour
{
    public GameEventCore m_Event;
    public UnityEvent m_Response;
}

public abstract class GameEventListenerCore<T> : MonoBehaviour
{
    public abstract GameEventCore<T> m_Event { get; set; }
    public abstract UnityEvent<T> m_Response { get; set; }
}
