using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    // Input handler
    private PlayerInput _input;

    // Event for each action
    public event Action<InputAction.CallbackContext> OnAttack;

    public event Action<InputAction.CallbackContext> OnMove;

    public event Action<InputAction.CallbackContext> OnDash;

    public event Action<InputAction.CallbackContext> OnPause;

    // Start is called before the first frame update
    private void Start()
    {
        // Bind *any* input to OnInput
        _input = GetComponent<PlayerInput>();
        _input.onActionTriggered += OnInput;
    }

    private void OnInput(InputAction.CallbackContext value)
    {
        // Check the name of the action to send the right event.
        switch (value.action.name)
        {
            case "Aim":
                OnAttack?.Invoke(value);
                break;
            case "Move":
                OnMove?.Invoke(value);
                break;
            case "Dash":
                OnDash?.Invoke(value);
                break;
            case "Pause":
                OnPause?.Invoke(value);
                break;
        }
    }
}
