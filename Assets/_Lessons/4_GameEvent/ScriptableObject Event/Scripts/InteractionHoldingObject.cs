using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Hanzs.Runtime.Interaction
{
    public class InteractionHoldingObject : AreaInteractObject
    {
        Transform m_OriginalParent;
        Rigidbody m_ParentRB;

        Vector3 m_PickupOffset;

        Sequence m_PickupTweenSequence;
        private void Awake()
        {
            _ObjectType = AreaInteractionType.Holding;

            m_OriginalParent = _ParentObject.transform.parent;
            m_ParentRB = _ParentObject.GetComponent<Rigidbody>();
            m_PickupOffset = -transform.localPosition;
        }

        public void Hold(AreaInteractObject previousObject, Transform handParent)
        {
            OnPickup(handParent);
            Debug.Log("拿");
        }
        public override void OnInteract()
        {
            base.OnInteract();
            if (m_MatchedObject)
            {
                m_MatchedObject.OnResetState();
                m_MatchedObject = null;
            }
        }

        public override void OnUnInteract()
        {
            OnDrop();

            Debug.Log("放");
        }

        public override void OnCombineToTarget(AreaInteractObject targetObject)
        {
            base.OnCombineToTarget(targetObject);
            m_ParentRB.isKinematic = true;
            _ParentObject.position = targetObject._ParentObject.position;
            _ParentObject.rotation = targetObject._ParentObject.rotation;
        }

        public void OnPickup(Transform parent)
        {
            _ParentObject.transform.SetParent(null);
            m_PickupTweenSequence = DOTween.Sequence();

            m_PickupTweenSequence.Insert(0, _ParentObject.transform.DOMove(parent.position + parent.TransformDirection(m_PickupOffset), 0.2f));
            m_PickupTweenSequence.Insert(0, _ParentObject.transform.DORotate(parent.rotation.eulerAngles, 0.2f));

            m_PickupTweenSequence.onComplete = () =>
               {
                   if (m_ParentRB)
                       m_ParentRB.isKinematic = true;

                   _ParentObject.transform.SetParent(parent);
                   _ParentObject.transform.localPosition = m_PickupOffset;
               };
        }

        public void OnDrop()
        {
            m_PickupTweenSequence.Kill();
            _ParentObject.transform.SetParent(m_OriginalParent);

            if (m_ParentRB)
            {
                m_ParentRB.isKinematic = false;
                //m_RootRB.velocity = velocity;
            }
        }


    }
}