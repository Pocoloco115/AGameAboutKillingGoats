using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Camera PlayerCamera;    
    [SerializeField] private float RotationSpeed = 200f;
    [SerializeField] private float MaxSpeedOnGround = 8f;
    [SerializeField] private float MinSpeedOnGround = 4f;
    [SerializeField] private float MaxSpeedInAir = 4f;
    [SerializeField] private float JumpForce = 9f;
    [SerializeField] private float GravityDownForce = 20f;
    [SerializeField] private Transform bulletOrigin;

    [Header("Animator")]
    [SerializeField] private Animator PlayerAnimator;

    private CharacterController m_Controller;
    private PlayerInputHandler m_InputHandler;
    private Vector3 CharacterVelocity;
    private float m_CameraVerticalAngle = 0f;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_InputHandler = GetComponent<PlayerInputHandler>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleCharacterMovement();
        HandleAnimations();
    }

    private void HandleCharacterMovement()
    {
        float lookHorizontal = m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * Time.deltaTime;
        float lookVertical = m_InputHandler.GetLookInputsVertical() * RotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * lookHorizontal);

        m_CameraVerticalAngle -= lookVertical;
        m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -90f, 90f);
        PlayerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0f, 0f);
        Vector3 moveInput = m_InputHandler.GetMoveInput();
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        float targetSpeed;
        if(m_Controller.isGrounded)
        {
            targetSpeed = m_InputHandler.GetSprintInputHeld() ? MaxSpeedOnGround : MinSpeedOnGround;
        }
        else
        {
            targetSpeed = MaxSpeedInAir;
        }

        Vector3 targetVelocity = moveDirection.normalized * targetSpeed;
        CharacterVelocity.x = targetVelocity.x;
        CharacterVelocity.z = targetVelocity.z;

        if (m_Controller.isGrounded)
        {
            if (m_InputHandler.GetJumpInputDown())
            {
                CharacterVelocity.y = JumpForce;
            }
            else
            {
                CharacterVelocity.y = -1f; 
            }
        }
        else
        {
            CharacterVelocity.y -= GravityDownForce * Time.deltaTime;
        }

        m_Controller.Move(CharacterVelocity * Time.deltaTime);
    }
    private void HandleAnimations()
    {
        if (PlayerAnimator == null)
        {
            return;
        }
        Vector3 horizontalVelocity = new Vector3(CharacterVelocity.x, 0f, CharacterVelocity.z);
        float speed = horizontalVelocity.magnitude;
        PlayerAnimator.SetBool("isIdle",
            speed < 0.1f);
        PlayerAnimator.SetBool("isWalking",
            speed >= 0.1f && speed <= MinSpeedOnGround);
        PlayerAnimator.SetBool("isRunning",
            speed >= MaxSpeedOnGround);
    }
}
