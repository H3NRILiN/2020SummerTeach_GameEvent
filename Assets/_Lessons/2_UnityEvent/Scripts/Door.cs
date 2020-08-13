namespace ISU.Lesson.UNITYEvent
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Door : MonoBehaviour
    {
        Animator m_Animator;
        private void Start()
        {
            m_Animator = GetComponent<Animator>();
        }

        public void SetOpenDoor(bool opened)
        {

            m_Animator.SetBool("Opened", opened);
        }
    }
}