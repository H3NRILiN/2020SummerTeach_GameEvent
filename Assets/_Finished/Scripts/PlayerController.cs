using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISUExample
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float m_RotationXSpeed;
        [SerializeField] float m_RotationYSpeed;
        [SerializeField] float m_DampTime;
        [SerializeField] float m_WalkSpeed;
        [SerializeField] float m_JumpHeight;

        Camera m_Camera;
        CharacterController m_CharacterController;
        ControllerInput m_Input;

        Vector3 m_DesiredMovement;
        Vector2 m_DesiredRotation;
        Vector2 m_RoationDampVelocity;
        void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = GetComponentInChildren<Camera>();
        }

        void Update()
        {

        }

        void ProcessInput()
        {
            m_Input.Horizontal = Input.GetAxis("Horizontal");
            m_Input.Vertical = Input.GetAxis("Vertical");
            m_Input.MouseX = Input.GetAxis("Mouse X");
            m_Input.MouseY = Input.GetAxis("Mouse Y");
            m_Input.Jump = Input.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            ProcessInput();
            Camera();
            Movement();
        }

        void Movement()
        {
            m_DesiredMovement.x = m_Input.Horizontal * m_WalkSpeed;
            m_DesiredMovement.z = m_Input.Vertical * m_WalkSpeed;
            m_DesiredMovement = transform.TransformDirection(m_DesiredMovement);

            if (m_CharacterController.isGrounded)
            {
                m_DesiredMovement.y = -2;
                if (m_Input.Jump)
                {
                    Jump();
                }

            }
            else
            {
                m_DesiredMovement.y += Physics.gravity.y * Time.deltaTime;
                // m_CharacterController.Move(m_Movement);
            }


            m_CharacterController.Move(m_DesiredMovement * Time.deltaTime);
        }

        void Jump()
        {
            m_DesiredMovement.y = Mathf.Sqrt(2 * -Physics.gravity.y * m_JumpHeight);
        }

        void Camera()
        {
            m_DesiredRotation.x += -m_Input.MouseY * m_RotationXSpeed * Time.deltaTime;
            m_DesiredRotation.y += m_Input.MouseX * m_RotationYSpeed * Time.deltaTime;

            var camRot = m_Camera.transform.rotation.eulerAngles;
            camRot.x = Mathf.SmoothDampAngle(camRot.x, m_DesiredRotation.x, ref m_RoationDampVelocity.x, m_DampTime);
            m_Camera.transform.rotation = Quaternion.Euler(camRot);

            var characterRot = transform.rotation.eulerAngles;
            characterRot.y = Mathf.SmoothDampAngle(characterRot.y, m_DesiredRotation.y, ref m_RoationDampVelocity.y, m_DampTime);
            transform.rotation = Quaternion.Euler(characterRot);
        }

        struct ControllerInput
        {
            public float Horizontal;
            public float Vertical;
            public float MouseX;
            public float MouseY;
            public bool Jump;
        }
    }
}
