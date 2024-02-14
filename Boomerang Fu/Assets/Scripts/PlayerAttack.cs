using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private BoomerangBehaviour _boomerangBehaviour;

    private void AttachBoomerang(BoomerangBehaviour boomerangBehaviour) // Permet d'attacher un boomerang à un joueur
    {
        _boomerangBehaviour = boomerangBehaviour;
    }

    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnAttack += Attack;
        _boomerangBehaviour = GetComponentInChildren<BoomerangBehaviour>();
    }
    void Attack(InputAction.CallbackContext context)
    {
        if(context.canceled && _boomerangBehaviour != null) {
            _boomerangBehaviour.StartAction(context);
            _boomerangBehaviour = null;
        }
    }
}
