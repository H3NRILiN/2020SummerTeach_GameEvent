using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class GameEventCore : ScriptableObject
{
    List<GameEventListenerCore> m_Listeners = new List<GameEventListenerCore>();

    //註冊
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

public abstract class GameEventListenerCore : MonoBehaviour
{
    public GameEventCore m_Event;
    public UnityEvent m_Response;
}

