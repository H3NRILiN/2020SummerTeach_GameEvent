using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISU.Lesson.GameEvent
{
    [RequireComponent(typeof(AreaInteraction))]
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] AreaInteractObjectEvent m_OnObjectFound;

        //========================================================
        //Holding狀態
        [SerializeField] Transform m_HoldingParent;
        //========================================================
        AreaInteraction m_Interactor;
        private void Awake()
        {
            m_Interactor = GetComponent<AreaInteraction>();

            m_Interactor.m_OnInteract += OnInteract;
            m_Interactor.m_OnUnInteract += OnUnInteract;
            m_Interactor.m_OnClosestObjectFound += m_OnObjectFound.Raise;
        }

        void OnInteract(AreaInteractObject currentObject, AreaInteractObject combineObject)
        {
            switch (currentObject.m_ObjectType)
            {
                case AreaInteractionType.Static:
                    {

                    }
                    break;
                case AreaInteractionType.Pickup:
                    break;
                case AreaInteractionType.Holding:
                    {
                        InteractionHoldingObject holding = currentObject as InteractionHoldingObject;
                        holding.Hold(combineObject, m_HoldingParent);
                    }
                    break;
            }
        }

        void OnUnInteract(AreaInteractObject obj)
        {
            obj.UnInteract();
        }
    }
}