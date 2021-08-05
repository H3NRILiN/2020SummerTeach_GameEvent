using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hanzs.Runtime.Interaction
{
    public class InteractionStaticObject : AreaInteractObject
    {
        [SerializeField] UnityEvent m_OnInteract;
        [SerializeField] UnityEvent m_OnUnInteract;
        private void Awake()
        {
            _ObjectType = AreaInteractionType.Static;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void OnInteract()
        {
            base.OnInteract();

            m_MatchedObject.m_MatchedObject = this;

            m_OnInteract.Invoke();
        }
        public override void OnUnInteract()
        {
        }

    }
}