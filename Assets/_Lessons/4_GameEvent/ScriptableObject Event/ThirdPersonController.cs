using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThirdPersonController : MonoBehaviour
{
    struct CInput
    {
        public float vertical;
        public float horizontal;
        public bool jump;
    }

    public enum AxisMode
    {
        RawAxis,
        Axis
    }
    //---------------------------------------------------------
    [SerializeField] CharacterController m_Controller;
    [SerializeField] AxisMode m_AxisMode;
    [SerializeField] float m_Speed = 10;
    [SerializeField] float m_JumpHeight = 1;
    //---------------------------------------------------------
    [Space(10)]
    [SerializeField] [Range(0, 1)] float m_RotationYDampTime = 0.1f;
    //---------------------------------------------------------
    [Space(10)]
    [SerializeField] Transform m_Model;
    [SerializeField] [Range(0, 1)] float m_ModelScaleDampTime = 0.1f;
    [SerializeField] float m_ModelOnJumpScaleMultiply = 2;
    [SerializeField] float m_ModelOnLandScaleMultiply = 0.1f;
    //---------------------------------------------------------
    [Space(10)]
    [SerializeField] [Range(0, 1)] float m_ModelTiltDampTime = 0.1f;
    [SerializeField] float m_ModelWalkingTiltZMultiply = -10;
    //---------------------------------------------------------
    [Space(10)]
    [SerializeField] ParticleSystem m_WalkParticle;
    [SerializeField] float m_WalkingCycleSpeed = 8;
    [SerializeField] float m_WalkingCyleScaleOffset = 0.15f;
    [SerializeField] [Range(0, 0.5f)] float m_WalkingSoundThreshold = 0.14f;
    [SerializeField] AudioSource m_WalkingSoundSource;
    [SerializeField] AudioClip[] m_WalkingSoundClips;
    [SerializeField] AudioClip m_JumpSound;
    [SerializeField] AudioClip m_LandSound;
    //---------------------------------------------------------
    [Space(10)]
    [SerializeField] bool m_ShowVector;
    //---------------------------------------------------------
    Vector3 m_DesiredMovement = new Vector3();
    Vector3 m_Movement = new Vector3();
    Vector3 m_Gravity;
    Vector3 m_ModelOriginalScale;
    Vector3 m_ScaleDampVelocity;
    Vector3 m_AngleDampVelocity;
    bool m_WasGrounded;
    bool m_WalkingSoundPlayed;
    float m_CurrentMagnitude = 0;
    //---------------------------------------------------------
    bool m_OnGround => m_WasGrounded && m_WasGrounded == m_Controller.isGrounded;
    bool m_OnAirborne => !m_WasGrounded && m_WasGrounded == m_Controller.isGrounded;
    bool m_OnJumping => m_WasGrounded && m_WasGrounded != m_Controller.isGrounded;
    bool m_OnLanding => !m_WasGrounded && m_WasGrounded != m_Controller.isGrounded;
    bool m_OnMoving => m_CurrentMagnitude > 0;
    //---------------------------------------------------------
    void Start()
    {
        m_Gravity = Physics.gravity;
        m_ModelOriginalScale = m_Model.localScale;
    }
    void Update()
    {
        Movement(GetInput());
    }

    CInput GetInput()
    {
        CInput input = new CInput();

        switch (m_AxisMode)
        {
            case AxisMode.RawAxis:
                input.horizontal = Input.GetAxisRaw("Horizontal");
                input.vertical = Input.GetAxisRaw("Vertical");
                break;
            case AxisMode.Axis:
                input.horizontal = Input.GetAxis("Horizontal");
                input.vertical = Input.GetAxis("Vertical");
                break;
        }

        input.jump = Input.GetButtonDown("Jump");
        return input;
    }

    void Movement(CInput input)
    {

        m_WasGrounded = m_Controller.isGrounded;
        //暫存方向數值
        m_DesiredMovement = new Vector3(input.horizontal, 0, input.vertical);
        m_DesiredMovement = m_DesiredMovement.normalized;
        m_CurrentMagnitude = m_DesiredMovement.magnitude;

        Rotate(m_DesiredMovement);


        //併到Movement，除了y(重力)
        m_Movement.x = m_DesiredMovement.x * m_Speed;
        m_Movement.z = m_DesiredMovement.z * m_Speed;


        if (m_OnGround)
        {
            m_Movement.y = -2f;

            if (input.jump)
            {
                //跳躍指令來源：https://youtu.be/_QajrabyTJc?t=1297
                m_Movement.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity.y);
            }
        }

        m_Movement += m_Gravity * Time.deltaTime;

        m_Controller.Move(m_Movement * Time.deltaTime);

        WalkCycleAndJump();
        WalkingVFX();
    }

    void Rotate(Vector3 direction)
    {
        float xAngle;
        float xDegree = 0;

        float yDegree;
        float yAngle;

        if (m_OnMoving)
        {
            var thisEuler = transform.rotation.eulerAngles;
            //旋轉指令來源：https://youtu.be/4HpC--2iowE?t=729
            yDegree = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            yAngle = Mathf.SmoothDampAngle(thisEuler.y, yDegree, ref m_AngleDampVelocity.y, m_RotationYDampTime);

            transform.rotation = Quaternion.Euler(thisEuler.x, yAngle, thisEuler.z);

            xDegree = m_CurrentMagnitude * m_ModelWalkingTiltZMultiply;
        }

        var modelEuler = m_Model.rotation.eulerAngles;

        xAngle = Mathf.SmoothDampAngle(modelEuler.x, xDegree, ref m_AngleDampVelocity.x, m_ModelTiltDampTime);

        m_Model.rotation = Quaternion.Euler(xAngle, modelEuler.y, modelEuler.z);
    }

    void WalkCycleAndJump()
    {
        var scale = m_Model.localScale;

        if (m_OnGround)
        {
            if (m_OnMoving)
            {
                var scaleCycle = Mathf.Abs(Mathf.Sin(Time.time * m_WalkingCycleSpeed) * m_WalkingCyleScaleOffset);
                scale.y = m_ModelOriginalScale.y - scaleCycle;
                if (scaleCycle >= m_WalkingSoundThreshold)
                {
                    WalkingSFX();
                }
            }
            else
            {
                scale = m_ModelOriginalScale;
            }
        }
        else if (m_OnAirborne)
        {
            scale = m_ModelOriginalScale;
        }


        if (m_OnJumping)
        {
            JumpingSFX();
            scale.y = m_ModelOriginalScale.y * m_ModelOnJumpScaleMultiply;

        }
        else if (m_OnLanding)
        {
            LandingSFX();
            scale.y = m_ModelOriginalScale.y * m_ModelOnLandScaleMultiply;
        }

        m_Model.localScale = Vector3.SmoothDamp(m_Model.localScale, scale, ref m_ScaleDampVelocity, m_ModelScaleDampTime);
    }

    private void WalkingVFX()
    {
        if (m_OnGround)
        {
            if (m_OnMoving)
            {
                if (!m_WalkParticle.isPlaying)
                    m_WalkParticle.Play();
            }
            else
            {
                if (m_WalkParticle.isPlaying)
                    m_WalkParticle.Stop();
            }

        }
        else
        {
            if (m_WalkParticle.isPlaying)
                m_WalkParticle.Stop();
        }
    }

    void WalkingSFX()
    {
        m_WalkingSoundSource.PlayOneShot(m_WalkingSoundClips[Random.Range(0, m_WalkingSoundClips.Length)]);
        m_WalkingSoundPlayed = true;
    }
    void JumpingSFX()
    {
        m_WalkingSoundSource.PlayOneShot(m_JumpSound);
    }
    void LandingSFX()
    {
        m_WalkingSoundSource.PlayOneShot(m_LandSound);
    }


    private void OnDrawGizmosSelected()
    {
        if (!m_ShowVector)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + m_Movement);
    }
    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.rigidbody)
    //         hit.rigidbody.velocity += m_Controller.velocity;
    // }
}
