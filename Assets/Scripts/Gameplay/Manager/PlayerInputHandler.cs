using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;

    public float LookSensitivity { get; private set; }

    void Awake()
    {
        ControlBindings.Reload(); 
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
        if (move.sqrMagnitude > 1f) move.Normalize();
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

    public bool GetJumpInputDown() => Input.GetKeyDown(ControlBindings.GetKey("Jump"));
    public bool GetSprintInputHeld() => Input.GetKey(ControlBindings.GetKey("Sprint")) && GetMoveInput() != Vector2.zero;
    public bool GetShootInputDown() => Input.GetKeyDown(ControlBindings.GetKey("Fire"));
    public bool GetReloadInputDown() => Input.GetKeyDown(ControlBindings.GetKey("Reload"));
    public bool GetCrouchInputToggled() => Input.GetKeyDown(ControlBindings.GetKey("Crouch"));
    public bool GetCrouchInputHeld() => Input.GetKey(ControlBindings.GetKey("Crouch"));
}