﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISU.Common
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PlayerController m_PlayerPrefab;
        static PlayerController m_Player;

        private void OnEnable()
        {
            GameState.m_PlayerCanMove = FPSPlayerCanMove;
            GameState.m_PlayerCursorLock = FPSCursorLock;
        }
        private void OnDisable()
        {
            GameState.m_PlayerCanMove -= FPSPlayerCanMove;
            GameState.m_PlayerCursorLock -= FPSCursorLock;
        }
        private void Awake()
        {
            m_Player = FindObjectOfType<PlayerController>();
            if (!m_Player)
                m_Player = Instantiate(m_Player);

            Application.targetFrameRate = 60;

            FPSCursorLock(true);
            Input.imeCompositionMode = IMECompositionMode.Off;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FPSCursorLock(bool on)
        {
            if (on)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void FPSPlayerCanMove(bool on)
        {
            if (on)
            {
                m_Player.m_CanMove = true;
            }
            else
            {
                m_Player.m_CanMove = false;
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
