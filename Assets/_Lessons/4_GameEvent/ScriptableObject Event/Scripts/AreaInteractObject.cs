using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hanzs.Runtime.Interaction
{
    [SelectionBase]
    public abstract class AreaInteractObject : MonoBehaviour
    {
        #region 物件集合
        static Dictionary<int, AreaInteractObject> m_Collection;
        public static AreaInteractObject GetObject(Transform _transform)
        {
            if (!_transform || !m_Collection.ContainsKey(_transform.GetInstanceID()))
                return null;
            return m_Collection[_transform.GetInstanceID()];
        }
        private void OnEnable()
        {
            if (m_Collection == null)
                m_Collection = new Dictionary<int, AreaInteractObject>();

            m_Collection.Add(_InstanceID, this);
        }
        private void OnDisable()
        {
            m_Collection.Remove(_InstanceID);
        }
        #endregion

        public bool _IsActive = true;
        public Transform _ParentObject;
        internal AreaInteractionType _ObjectType;
        public AreaInteractionState _InteractionRequirment;
        public bool _RequiredMatchObject;
        public string _MatchID;
        public bool _DisableMatchObjectWhenInteract;
        public bool _DisableThisObjectWhenInteract;
        public bool _ShowMatchInInspector;
        public AreaInteractObject m_MatchedObject;

        private void Start()
        {
            if (!_ParentObject)
                _ParentObject = this.transform;
        }

        public int _InstanceID { get => transform.GetInstanceID(); }

        public void OnBeforeInteract(AreaInteractObject anotherObject)
        {
            m_MatchedObject = anotherObject;

        }

        public virtual void OnInteract()
        {
            if (_DisableMatchObjectWhenInteract)
            {
                m_MatchedObject._IsActive = false;
            }

            if (_DisableThisObjectWhenInteract)
            {
                _IsActive = false;
            }
        }

        public abstract void OnUnInteract();

        public virtual void OnResetState()
        {
            if (_DisableMatchObjectWhenInteract)
            {
                Debug.Log("AAA");
                m_MatchedObject._IsActive = true;
            }

            if (_DisableThisObjectWhenInteract)
            {
                _IsActive = true;
            }
        }

        /// <summary>
        /// 與其他物件組合
        /// </summary>
        /// <param name="anotherObject">其他物件</param>
        public virtual void OnCombineToTarget(AreaInteractObject anotherObject)
        {
            Debug.Log($"目前物件: {_MatchID} | 結合物件: {(anotherObject == null ? "無" : anotherObject._MatchID) }");
        }

        public virtual void OnCombinationCheck()
        {
            if (m_MatchedObject)
            {
                m_MatchedObject.OnCombineToTarget(this);
            }
        }

        public void SetIsActive(bool active)
        {
            _IsActive = active;
        }
    }
}