using System.Collections;
using System.Collections.Generic;
using ISU.Example.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace ISU.Test.Events
{
    public class GameEventListener_Item : GameEventListenerCore<ItemObject>
    {
        public GameEvent_Item gameEvent;
        public UnityEvent_Item response;
        public override GameEventCore<ItemObject> m_Event
        {
            get { return gameEvent; }
            set { gameEvent = (GameEvent_Item)value; }
        }
        public override UnityEvent<ItemObject> m_Response
        {
            get { return response; }
            set { response = (UnityEvent_Item)value; }
        }
    }

}