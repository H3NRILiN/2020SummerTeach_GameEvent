using System.Collections;
using System.Collections.Generic;
using ISU.Example.Inventory;
using UnityEngine;

namespace ISU.Test.Events
{
    [CreateAssetMenu(menuName = "_Finished/GameEvent/NewGameEvent_Item")]
    public class GameEvent_Item : GameEventCore<ItemObject>
    {
    }
}