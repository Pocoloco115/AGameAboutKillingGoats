using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    public float LookSensitivity = 1f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
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
}