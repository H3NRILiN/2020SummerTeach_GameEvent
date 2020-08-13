namespace ISU.Lesson.UNITYEvent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Interactor : MonoBehaviour
    {
        [SerializeField] Camera m_Cam;

        [SerializeField] LayerMask m_RayMask;
        [SerializeField] CameraAnimation m_CameraAnimation;

        bool m_Interacting;
        IInteractable m_CurrentInteracting;
        Transform m_LastInteracted;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_CameraAnimation.ProcessCameraFollowing();

            ProcessInteraction();

            if (m_Interacting)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    m_CurrentInteracting.OnExitInteraction();

                    m_Interacting = false;

                    m_CurrentInteracting = null;
                }
            }
        }



        void ProcessInteraction()
        {
            RaycastHit hit;
            if (Physics.Raycast(m_Cam.transform.position, m_Cam.transform.forward, out hit, 3, m_RayMask))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    if (!m_Interacting)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            m_CurrentInteracting = hit.transform.GetComponent<IInteractable>();

                            m_CurrentInteracting.OnInteract();

                            m_CameraAnimation.MoveToTarget(m_CurrentInteracting.m_CamAnimTarget);

                            m_Interacting = true;
                        }
                    }
                }
            }
        }
    }
}