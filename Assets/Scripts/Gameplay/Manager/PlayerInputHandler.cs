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

    public float LookSensitivity = 1f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        shootAction = playerInput.actions["Attack"];
        reloadAction = playerInput.actions["Reload"];
        crouchAction = playerInput.actions["Crouch"];
    }

    public Vector2 GetMoveInput()
    {
        return moveAction.ReadValue<Vector2>();
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
        return jumpAction.WasPressedThisFrame();
    }

    public bool GetSprintInputHeld()
    {
        return sprintAction.IsPressed() && GetMoveInput() != Vector2.zero;
    }

    public bool GetShootInputDown()
    {
        return shootAction.WasPressedThisFrame();
    }
    public bool GetReloadInputDown()
    {
        return reloadAction.WasPressedThisFrame();
    }
    public bool GetCrouchInputToggled()
    {
        return crouchAction.WasPressedThisFrame();
    }
}