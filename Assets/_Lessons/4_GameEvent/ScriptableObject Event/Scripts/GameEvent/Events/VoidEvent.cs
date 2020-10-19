using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    [CreateAssetMenu(menuName = GameEventUtility.assetMenuPath + "Void")]
    public class VoidEvent : GameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}