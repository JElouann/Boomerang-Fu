using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody Rb;

    public float VitesseDash;

    private void Awake()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnDash += Dasher;
    }
    void Dasher(InputAction.CallbackContext Dash)
    {
        if (Dash.performed) { 
            Rb.AddForce(Vector3.forward * VitesseDash, ForceMode.Impulse);
        }
    }

}
