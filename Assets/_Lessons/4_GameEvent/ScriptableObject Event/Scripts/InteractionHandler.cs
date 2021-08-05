using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanzs.Runtime.Interaction
{
    [RequireComponent(typeof(AreaInteraction))]
    public class InteractionHandler : MonoBehaviour
    {
        //[SerializeField] AreaInteractObjectEvent m_OnObjectFound;

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
            //  m_Interactor.m_OnClosestObjectFound += m_OnObjectFound.Raise;
        }

        void OnInteract(AreaInteractObject currentObject, AreaInteractObject previousObject)
        {
            //通用互動
            currentObject.OnBeforeInteract(previousObject);
            currentObject.OnInteract();
            //組合互動
            currentObject.OnCombinationCheck();

            //如果有前一個物件就不執行專屬互動
            if (previousObject) return;

            //腳本專屬互動
            switch (currentObject._ObjectType)
            {
                case AreaInteractionType.Static:
                    {
                        //靜止物件  
                    }
                    break;
                case AreaInteractionType.Pickup:
                    {
                        //撿起物件
                    }
                    break;
                case AreaInteractionType.Holding:
                    {
                        //手拿物件
                        InteractionHoldingObject holding = currentObject as InteractionHoldingObject;
                        holding.Hold(previousObject, m_HoldingParent);
                    }
                    break;
            }
        }

        void OnUnInteract(AreaInteractObject obj)
        {
            obj.OnUnInteract();
        }
    }
}