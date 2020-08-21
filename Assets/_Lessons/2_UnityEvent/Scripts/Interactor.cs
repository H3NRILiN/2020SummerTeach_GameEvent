namespace ISU.Lesson.UNITYEvent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class Interactor : MonoBehaviour
    {
        [SerializeField] Camera m_Cam;

        [SerializeField] LayerMask m_RayMask;
        [SerializeField] UnityEventInteractable m_OnInteract;

        bool m_CanExit;
        bool m_Interacting;
        IInteractable m_CurrentInteracting;
        Transform m_LastInteracted;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ProcessInteraction();

            // if (m_Interacting)
            // {
            //     if (Input.GetKeyDown(KeyCode.Escape))
            //     {
            //         m_CurrentInteracting.OnExitInteraction();
            //     }
            // }
        }

        private void ExitInteraction()
        {
            m_Interacting = false;

            m_CurrentInteracting = null;
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

                            m_CurrentInteracting.OnExitInteractionEvent = ExitInteraction;

                            m_OnInteract.Invoke(m_CurrentInteracting);

                            m_Interacting = true;
                        }
                    }
                }
            }
        }

        [Serializable]
        public class UnityEventInteractable : UnityEvent<IInteractable>
        {

        }
    }
}