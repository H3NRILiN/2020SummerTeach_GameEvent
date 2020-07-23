using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ISUExample
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
    }
}
