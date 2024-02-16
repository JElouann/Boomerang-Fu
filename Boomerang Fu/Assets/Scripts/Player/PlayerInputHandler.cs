using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    public event Action<InputAction.CallbackContext> OnAttack;
    public event Action<InputAction.CallbackContext> OnMove;
    public event Action<InputAction.CallbackContext> OnDash;
    public event Action<InputAction.CallbackContext> OnPause;

    public event Action<InputAction.CallbackContext> OnOtherInput;

    private PlayerInput _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _input.onActionTriggered += onInput;
    }

    public void onInput(InputAction.CallbackContext value){
        switch(value.action.name){
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
