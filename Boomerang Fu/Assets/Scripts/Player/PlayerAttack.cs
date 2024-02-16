using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BoomerangCollision _boomerangCollision { get; private set; }

    private void AttachBoomerang(BoomerangCollision boomerangBehaviour) // Permet d'attacher un boomerang � un joueur
    {
        _boomerangCollision = boomerangBehaviour;
        boomerangBehaviour.GetComponent<Renderer>().materials[0].SetColor("_Color", GameManager.Instance.Color[GetComponent<PlayerMain>().id]) ;
    }

    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnAttack += Attack;
        _boomerangCollision = GetComponentInChildren<BoomerangCollision>();
    }

    void Attack(InputAction.CallbackContext context)
    {
        if(context.canceled && _boomerangCollision != null) {
            _boomerangCollision.GetComponent<BoomerangMovement>().StartAction();
            _boomerangCollision = null;
        }
    }
}
