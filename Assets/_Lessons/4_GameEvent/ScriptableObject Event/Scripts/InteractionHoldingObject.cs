using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    public class InteractionHoldingObject : AreaInteractObject
    {

        private void Awake()
        {
            m_ObjectType = AreaInteractionType.Holding;
        }

        public void Hold(AreaInteractObject last, Transform handParent)
        {
            // return;
            m_ParentObject.SetParent(handParent);
            Debug.Log("拿");
        }

        public override void UnInteract()
        {
            m_ParentObject.SetParent(null);
            Debug.Log("放");
        }
    }
}