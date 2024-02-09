using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    public Rigidbody Rb;
    private PlayerDash _dash;
    public float Vitesse = 5;
    private void Awake()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnMove += Movement;
        _dash = GetComponent<PlayerDash>();
    }
    void Movement(InputAction.CallbackContext Moving)
    {
        if(_dash._timer <= _dash.Cooldown - (_dash.Cooldown / 4)) { 
            var dir = Moving.ReadValue<Vector2>();
            Rb.velocity = (new Vector3(dir.x, 0, dir.y) * Vitesse);
        }
    }

    private void FixedUpdate()
    {
        if (Rb.velocity.magnitude > 0.1) { 
            this.transform.rotation = Quaternion.LookRotation(Rb.velocity, Vector3.up);
        }
    }
}
