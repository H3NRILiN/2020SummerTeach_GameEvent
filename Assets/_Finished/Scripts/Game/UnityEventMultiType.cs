using System;
using ISU.Example.Inventory;
using UnityEngine.Events;

namespace ISU.Test.Events
{
    [Serializable]
    public class UnityEvent_Item : UnityEvent<ItemObject>
    {
    }
}