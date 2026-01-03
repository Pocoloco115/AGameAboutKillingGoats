using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction shootAction;
    private InputAction reloadAction;
    private InputAction crouchAction;

    public float LookSensitivity { get; private set; }

    void Awake()
    {
        ControlBindings.Load();
        LookSensitivity = ControlBindings.Sensitivity;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
    }
    public Vector2 GetMoveInput()
    {
        float x = ControlBindings.GetAxis("Move Left", "Move Right");
        float y = ControlBindings.GetAxis("Move Backward", "Move Forward");

        Vector2 move = new Vector2(x, y);

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        return move;
    }

    public float GetLookInputsHorizontal()
    {
        return lookAction.ReadValue<Vector2>().x * LookSensitivity;
    }

    public float GetLookInputsVertical()
    {
        return lookAction.ReadValue<Vector2>().y * LookSensitivity;
    }

    public bool GetJumpInputDown()
    {
        return Input.GetKeyDown(ControlBindings.GetKey("Jump"));
    }

    public bool GetSprintInputHeld()
    {
        return Input.GetKey(ControlBindings.GetKey("Sprint")) &&
               GetMoveInput() != Vector2.zero;
    }

    public bool GetShootInputDown()
    {
        return Input.GetKeyDown(ControlBindings.GetKey("Fire"));
    }

    public bool GetReloadInputDown()
    {
        return Input.GetKeyDown(ControlBindings.GetKey("Reload"));
    }

    public bool GetCrouchInputToggled()
    {
        return Input.GetKeyDown(ControlBindings.GetKey("Crouch"));
    }
    public bool GetCrouchInputHeld()
    {
        return Input.GetKey(ControlBindings.GetKey("Crouch"));
    }
}