using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    public class InteractionHoldingObject : AreaInteractObject
    {
        [SerializeField] Transform m_Root;
        private void Awake()
        {
            m_ObjectType = AreaInteractionType.Holding;
        }

        public void Hold(AreaInteractObject last, Transform handParent)
        {
            m_Root.SetParent(handParent);
            Debug.Log("拿");
        }

        public override void UnInteract()
        {
            m_Root.SetParent(null);
            Debug.Log("放");
        }
    }
}