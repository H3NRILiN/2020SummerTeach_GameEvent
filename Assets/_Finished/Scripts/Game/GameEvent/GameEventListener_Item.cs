using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Item : GameEventListenerCore<GameItem>
{
    public GameEvent_Item gameEvent;
    public UnityEvent_Item response;
    public override GameEventCore<GameItem> m_Event
    {
        get { return gameEvent; }
        set { gameEvent = (GameEvent_Item)value; }
    }
    public override UnityEvent<GameItem> m_Response
    {
        get { return response; }
        set { response = (UnityEvent_Item)value; }
    }
}

