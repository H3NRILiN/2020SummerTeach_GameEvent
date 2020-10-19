using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ISU.Lesson.GameEvent
{
    public class AreaInteractionObject : MonoBehaviour
    {
        [SerializeField] GameObject m_Root;

        Transform m_OriginalParent;
        Rigidbody m_RootRB;

        Vector3 m_PickupOffset;

        Sequence m_PickupTweenSequence;
        private void Awake()
        {
            m_OriginalParent = m_Root.transform.parent;
            m_RootRB = m_Root.GetComponent<Rigidbody>();

            m_PickupOffset = -transform.localPosition;
        }

        public void OnPickup(Transform parent)
        {
            m_Root.transform.SetParent(null);
            m_PickupTweenSequence = DOTween.Sequence();

            m_PickupTweenSequence.Insert(0, m_Root.transform.DOMove(parent.position + parent.TransformDirection(m_PickupOffset), 0.2f));
            m_PickupTweenSequence.Insert(0, m_Root.transform.DORotate(parent.rotation.eulerAngles, 0.2f));

            m_PickupTweenSequence.OnComplete(() =>
             {
                 if (m_RootRB)
                     m_RootRB.isKinematic = true;

                 m_Root.transform.SetParent(parent);
                 m_Root.transform.localPosition = m_PickupOffset;
             });



        }

        public void OnDrop(Vector3 velocity)
        {
            m_PickupTweenSequence.Kill();
            m_Root.transform.SetParent(m_OriginalParent);

            if (m_RootRB)
            {
                m_RootRB.isKinematic = false;
                m_RootRB.velocity = velocity;
            }
        }
    }
}