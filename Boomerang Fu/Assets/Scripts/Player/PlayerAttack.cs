using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BoomerangBehaviour _boomerangBehaviour { get; private set; }

    private Vector2 _aim;

    private void AttachBoomerang(BoomerangBehaviour boomerangBehaviour) // Permet d'attacher un boomerang � un joueur
    {
        _boomerangBehaviour = boomerangBehaviour;

        boomerangBehaviour.GetComponent<Renderer>().materials[0].SetColor("_Color", GameManager.Instance.Color[GetComponent<PlayerMain>().id]) ;
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
            _boomerangBehaviour.StartAction(_aim);
            _boomerangBehaviour = null;
        }
        else
        {
            _aim = context.ReadValue<Vector2>();
        }
    }
}
