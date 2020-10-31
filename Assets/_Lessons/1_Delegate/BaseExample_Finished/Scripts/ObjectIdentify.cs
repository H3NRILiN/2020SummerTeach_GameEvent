using UnityEngine;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class ObjectIdentify : MonoBehaviour
    {
        DelegateTest m_DelegateTest;
        ActionFuncTest m_ActionFuncTest;
        [SerializeField] string m_MyName = "物件";
        private void Start()
        {
            m_DelegateTest = FindObjectOfType<DelegateTest>();
            m_DelegateTest.m_OnDoThing += SayMyName;

            m_ActionFuncTest = FindObjectOfType<ActionFuncTest>();
            m_ActionFuncTest.m_OnDoThing += SayMyName;
        }
        private void OnEnable()
        {
            //物件開啟時觸發
        }
        private void OnDisable()
        {
            //物件關閉時觸發
            m_DelegateTest.m_OnDoThing -= SayMyName;
            m_ActionFuncTest.m_OnDoThing -= SayMyName;
        }

        public void SayMyName()
        {
            Debug.Log($"我是{m_MyName}");
        }
    }
}