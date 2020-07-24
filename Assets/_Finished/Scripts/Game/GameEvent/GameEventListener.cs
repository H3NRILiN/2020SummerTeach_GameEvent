using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListener : GameEventListenerCore
{
    private void OnEnable()
    {
        m_Event.Register(this);
    }

    private void OnDisable()
    {
        m_Event.UnRegister(this);
    }
}
