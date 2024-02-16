using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerDash))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerDash _dash;
    private PlayerInput _input;

    [SerializeField]
    private float _vitesse = 5;

    private void Awake()
    {
        _input = GetComponentInChildren<PlayerInput>();
        _dash = GetComponent<PlayerDash>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_dash.Timer <= _dash.CoolDown - (_dash.CoolDown / 4))
        {
            var dir = _input.actions.FindAction("Move").ReadValue<Vector2>();
            _rb.velocity = new Vector3(dir.x, 0, dir.y) * _vitesse;
        }

        if (_rb.velocity.magnitude > 0.3)
        {
            Quaternion q = Quaternion.LookRotation(_rb.velocity, Vector3.up);
            q.eulerAngles = new Vector3(0, q.eulerAngles.y, 0);
            transform.rotation = q;
        }
    }
}
