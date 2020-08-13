using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ISU.Common
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PlayerController m_PlayerPrefab;
        static PlayerController m_Player;

        public static GameManager instence;
        private void Awake()
        {
            instence = this;

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
                m_Player.m_CanMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                m_Player.m_CanMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
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
