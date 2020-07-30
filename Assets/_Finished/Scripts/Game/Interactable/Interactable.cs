using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ISU.Example
{
    [RequireComponent(typeof(Collider))]
    [SelectionBase]
    public class Interactable : MonoBehaviour
    {
        public string m_ObjectName;
        //顏色 (用於字等等的)
        public Color m_TextColor = Color.white;
        //使用按鍵互動?
        public bool m_UseKeyPress = false;
        //互動時觸發事件
        [SerializeField] UnityEvent m_Event;
        //Pickup參考, 決定物件是否為Pickup *新增Pickup時自動加入
        public SubInteractor m_SubInteractor;
        private void Reset()
        {
            //自動設置Tag
            this.tag = "Interactable";
        }
        void Start()
        {
            //註冊進Interaction Manager
            InteractionManager.m_Instance.Register(this);
        }

        //開始互動
        public void OnInteract()
        {
            //Debug.Log($"與{m_ObjectName}互動");

            //觸發事件
            m_Event.Invoke();

            if (m_SubInteractor != null)
            {
                //觸發Pickup動作
                m_SubInteractor.OnInteract();
            }
        }

        private void OnDestroy()
        {
            //取消註冊
            InteractionManager.m_Instance.UnRegister(this);
        }

        private void OnDrawGizmosSelected()
        {
            var eventCount = m_Event.GetPersistentEventCount();
            if (eventCount > 0)
            {
                for (int i = 0; i < eventCount; i++)
                {
                    if (m_Event.GetPersistentTarget(i) && m_Event.GetPersistentTarget(i) is Component)
                    {
                        var pos = ((Component)m_Event.GetPersistentTarget(i)).transform.position;
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(transform.position, pos);
                        Gizmos.DrawSphere(pos, .05f);

                    }

                }
            }
        }
    }
}
