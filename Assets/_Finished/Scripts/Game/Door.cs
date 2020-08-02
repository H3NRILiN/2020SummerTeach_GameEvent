using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Door : MonoBehaviour
{
    Light m_Light;
    Animator m_Animator;
    bool m_Opened = false;

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Light = GetComponentInChildren<Light>();
    }

    public void DoorControl()
    {
        m_Opened = !m_Opened;
        m_Animator.SetBool("Opened", m_Opened);

        m_Light.color = m_Opened ? Color.green : Color.red;
    }
}
