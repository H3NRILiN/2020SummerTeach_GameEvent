using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator m_Animator;
    bool m_Opened = false;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void DoorControl()
    {
        m_Opened = !m_Opened;
        m_Animator.SetBool("Opened", m_Opened);
    }
}
