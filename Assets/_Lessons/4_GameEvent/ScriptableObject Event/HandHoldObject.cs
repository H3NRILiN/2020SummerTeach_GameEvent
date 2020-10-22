using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ISU.Lesson.GameEvent
{
    [SelectionBase]
    public class HandHoldObject : MonoBehaviour
    {

        static Dictionary<int, HandHoldObject> collection;
        static Dictionary<int, HandHoldObject> m_Collection
        {
            get
            {
                if (collection == null)
                    collection = new Dictionary<int, HandHoldObject>();

                return collection;
            }
            set
            {

                collection = value;
            }
        }
        public static HandHoldObject GetObject(int instenceID)
        {
            if (m_Collection.ContainsKey(instenceID))
            {
                return m_Collection[instenceID];
            }

            return null;
        }



        public DetectionMask m_DetectionMask;
        [SerializeField] GameObject m_Root;
        public string m_MatchID;
        Transform m_OriginalParent;
        Rigidbody m_RootRB;

        Vector3 m_PickupOffset;

        Sequence m_PickupTweenSequence;

        private void OnEnable()
        {
            m_Collection.Add(transform.GetInstanceID(), this);
        }
        private void OnDisable()
        {
            m_Collection.Remove(transform.GetInstanceID());
        }

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

            m_PickupTweenSequence.onComplete = () =>
               {
                   if (m_RootRB)
                       m_RootRB.isKinematic = true;

                   m_Root.transform.SetParent(parent);
                   m_Root.transform.localPosition = m_PickupOffset;
               };
        }

        public void OnDrop()
        {
            m_PickupTweenSequence.Kill();
            m_Root.transform.SetParent(m_OriginalParent);

            if (m_RootRB)
            {
                m_RootRB.isKinematic = false;
                //m_RootRB.velocity = velocity;
            }
        }

        public void OnDropZone(Transform zone, System.Action onTweenComplete)
        {

            m_RootRB.isKinematic = true;
            gameObject.layer = 0;

            m_PickupTweenSequence = DOTween.Sequence();
            m_PickupTweenSequence.SetEase(Ease.InOutCubic);
            m_PickupTweenSequence.Insert(0, m_Root.transform.DOMove(zone.position, 1));
            m_PickupTweenSequence.Insert(0, m_Root.transform.DORotate(zone.rotation.eulerAngles, 1));

            m_PickupTweenSequence.onComplete = () => onTweenComplete?.Invoke();
        }
    }
}