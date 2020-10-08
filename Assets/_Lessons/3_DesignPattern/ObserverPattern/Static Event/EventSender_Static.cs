using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSender_Static : MonoBehaviour
{
    public static event Action m_OnEventSend;
    public static event Action<Transform> m_OnRegisterEvent;
    public static event Action m_OnEventReset;

    private void Start()
    {
        m_OnRegisterEvent?.Invoke(transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_OnEventSend?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            m_OnEventReset?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
