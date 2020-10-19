using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ISU.Lesson.DesignPattern.Observer
{
    public class EventSender_Static : MonoBehaviour
    {
        [SerializeField] SpriteRenderer m_Sprite;
        [SerializeField] Color m_SendingColor;
        public static event Action<Color> m_OnEventSend;
        public static event Action<Transform> m_OnRegisterEvent;
        public static event Action m_OnEventReset;

        private void OnValidate()
        {
            m_Sprite.color = m_SendingColor;
        }

        private void Start()
        {
            m_OnRegisterEvent?.Invoke(transform);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_OnEventSend?.Invoke(m_SendingColor);
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
}