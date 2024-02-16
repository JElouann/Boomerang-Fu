using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;

    public event Action<InputAction.CallbackContext> OnAttack;

    public event Action<InputAction.CallbackContext> OnMove;

    public event Action<InputAction.CallbackContext> OnDash;

    public event Action<InputAction.CallbackContext> OnPause;

    public event Action<InputAction.CallbackContext> OnOtherInput;

    // Start is called before the first frame update
    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _input.onActionTriggered += OnInput;
    }

    private void OnInput(InputAction.CallbackContext value)
    {
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
            default:
                OnOtherInput?.Invoke(value);
                break;
        }
    }
}
