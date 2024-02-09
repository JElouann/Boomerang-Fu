using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private BoomerangBehaviour _boomerangBehaviour;
    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnAttack += Attack;
        _boomerangBehaviour = GetComponentInChildren<BoomerangBehaviour>();
    }
    void Attack(InputAction.CallbackContext context)
    {
        if(context.performed && _boomerangBehaviour != null) {
            _boomerangBehaviour.StartAction(context);
            _boomerangBehaviour = null;
        }
    }
}
