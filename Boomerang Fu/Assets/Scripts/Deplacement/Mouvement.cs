using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    public Rigidbody Rb;
    private PlayerDash _dash;
    private PlayerInput _input;
    public float Vitesse = 5;
    private void Awake()
    {
        _input = GetComponentInChildren<PlayerInput>();
        _dash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        if(_dash._timer <= _dash.Cooldown - (_dash.Cooldown / 4)) { 
            var dir = _input.actions.FindAction("Move").ReadValue<Vector2>();
            Rb.velocity = (new Vector3(dir.x, 0, dir.y) * Vitesse);
        }

        if (Rb.velocity.magnitude > 0.3)
        {
            Quaternion q = Quaternion.LookRotation(Rb.velocity, Vector3.up);
            q.eulerAngles = new Vector3(0, q.eulerAngles.y, 0);
            this.transform.rotation = q;
        }
    }
}
