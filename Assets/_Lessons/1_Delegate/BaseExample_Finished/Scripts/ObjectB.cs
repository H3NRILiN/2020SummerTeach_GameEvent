using UnityEngine;

namespace ISU.Lesson.Delegate.BaseExample
{
    public class ObjectB : MonoBehaviour
    {
        public void ShowMessage()
        {
            Debug.Log($"我是{name}");
        }
    }
}