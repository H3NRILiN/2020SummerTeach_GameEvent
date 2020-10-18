using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    struct CInput
    {
        public float vertical;
        public float horizontal;
        public bool jump;
    }
    [SerializeField] CharacterController m_Controller;
    [SerializeField] float m_Speed = 10;
    [SerializeField] float m_JumpHeight = 1;
    [SerializeField] [Range(0, 1)] float m_RotationYDampTime = 0.1f;
    [SerializeField] Transform m_Model;
    [SerializeField] [Range(0, 1)] float m_ModelScaleDampTime = 0.1f;
    [SerializeField] float m_ModelOnJumpScaleMultiply = 2;
    [SerializeField] float m_ModelOnLandScaleMultiply = 0.1f;
    [SerializeField] [Range(0, 1)] float m_ModelTiltDampTime = 0.1f;
    [SerializeField] float m_ModelWalkingTiltZMultiply = -10;

    Vector3 m_Movement = new Vector3();
    Vector3 m_Gravity;
    Vector3 m_ModelOriginalScale;
    Vector3 m_ScaleDampVelocity;
    Vector3 m_AngleDampVelocity;
    bool m_WasGrounded;

    float m_CurrentMagnitude = 0;

    bool m_OnGround => m_WasGrounded && m_WasGrounded == m_Controller.isGrounded;
    bool m_OnAirborne => !m_WasGrounded && m_WasGrounded == m_Controller.isGrounded;
    bool m_OnJumping => m_WasGrounded && m_WasGrounded != m_Controller.isGrounded;
    bool m_OnLanding => !m_WasGrounded && m_WasGrounded != m_Controller.isGrounded;

    bool m_OnMoving => m_CurrentMagnitude > 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Gravity = Physics.gravity;
        m_ModelOriginalScale = m_Model.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Movement(GetInput());
    }
    CInput GetInput()
    {
        CInput input;
        input.horizontal = Input.GetAxisRaw("Horizontal");
        input.vertical = Input.GetAxisRaw("Vertical");
        input.jump = Input.GetButtonDown("Jump");
        return input;
    }
    void Movement(CInput input)
    {

        m_WasGrounded = m_Controller.isGrounded;
        //暫存方向數值
        Vector3 desiredMovement = new Vector3(input.horizontal, 0, input.vertical);
        desiredMovement = desiredMovement.normalized;
        m_CurrentMagnitude = desiredMovement.magnitude;

        Rotate(desiredMovement);


        //併到Movement，除了y(重力)
        m_Movement.x = desiredMovement.x * m_Speed;
        m_Movement.z = desiredMovement.z * m_Speed;


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

        WalkCycle();
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

    void WalkCycle()
    {
        var scale = m_Model.localScale;

        if (m_OnGround)
        {
            if (m_OnMoving)
            {
                scale.y = m_ModelOriginalScale.y - Mathf.Abs(Mathf.Sin(Time.time * 8) * 0.15f);
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
            scale.y = m_ModelOriginalScale.y * m_ModelOnJumpScaleMultiply;
        }
        else if (m_OnLanding)
        {
            scale.y = m_ModelOriginalScale.y * m_ModelOnLandScaleMultiply;
        }

        m_Model.localScale = Vector3.SmoothDamp(m_Model.localScale, scale, ref m_ScaleDampVelocity, m_ModelScaleDampTime);
    }


    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.rigidbody)
    //         hit.rigidbody.velocity += m_Controller.velocity;
    // }
}
