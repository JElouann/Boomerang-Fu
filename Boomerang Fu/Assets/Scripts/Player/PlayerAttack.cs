using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BoomerangCollision _boomerangBehaviour { get; private set; }

    private void AttachBoomerang(BoomerangCollision boomerangBehaviour) // Permet d'attacher un boomerang � un joueur
    {
        _boomerangBehaviour = boomerangBehaviour;

        boomerangBehaviour.GetComponent<Renderer>().materials[0].SetColor("_Color", GameManager.Instance.Color[GetComponent<PlayerMain>().id]) ;
    }

    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnAttack += Attack;
        _boomerangBehaviour = GetComponentInChildren<BoomerangCollision>();
    }

    void Attack(InputAction.CallbackContext context)
    {
        if(context.canceled && _boomerangBehaviour != null) {
            _boomerangBehaviour.GetComponent<BoomerangMovement>().StartAction();
            _boomerangBehaviour = null;
        }
    }
}
