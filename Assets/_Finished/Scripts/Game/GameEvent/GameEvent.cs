using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ISU.Example.Events
{
    [CreateAssetMenu(menuName = "_Finished/GameEvent/NewGameEvent")]
    public class GameEvent : ScriptableObject
    {

        List<GameEventListener> m_Listeners = new List<GameEventListener>();

        //註冊
        public void Register(GameEventListener listener)
        {
            m_Listeners.Add(listener);
        }

        public void UnRegister(GameEventListener listener)
        {
            m_Listeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = 0; i < m_Listeners.Count; i++)
            {
                m_Listeners[i].m_Response.Invoke();
            }
        }

    }
}