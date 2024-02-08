using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       var _input = GetComponentInChildren<PlayerInputHandler>();
       _input.OnMove += Move;
       _input.OnPause += Pause;
       _input.OnAttack += Aim;
       _input.OnDash += Dash;
    }

    void Move(InputAction.CallbackContext value)
    {
        Debug.Log("Move");
    }

    void Pause(InputAction.CallbackContext value)
    {
        Debug.Log("Pause");
    }

    void Aim(InputAction.CallbackContext value)
    {
        Debug.Log("Aim");
    }

    void Dash(InputAction.CallbackContext value)
    {
        Debug.Log("Dash");
    }

}
