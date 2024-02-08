using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    public Rigidbody Rb;
    public float Vitesse = 5;
    private void Awake()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnMove += Movement;
    }
    void Movement(InputAction.CallbackContext Moving)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Rb.MovePosition(new Vector3(x,transform.position.y,z) * Vitesse * Time.deltaTime);
    }
}
