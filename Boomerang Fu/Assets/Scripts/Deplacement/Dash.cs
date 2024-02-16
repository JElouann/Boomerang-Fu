using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public Rigidbody Rb;

    public float VitesseDash;

    public float Cooldown;

    public float _timer = 0;

    [SerializeField] private GameObject _dashVFX;

    private AudioSource _src;

    private void Awake()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnDash += Dasher;
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
    }

    void Dasher(InputAction.CallbackContext Dash)
    {
        if (Dash.performed && _timer < 0)
        {
            _src.Play();
            _timer = Cooldown;
            GameObject _dashVFXClone = Instantiate(_dashVFX, transform);
            _dashVFXClone.transform.parent = null;
            StartCoroutine(DestructVFX(_dashVFXClone));
            Rb.velocity = Vector3.zero;
            Rb.AddRelativeForce(Vector3.forward * VitesseDash * 5, ForceMode.Impulse);
        }
    }

    private IEnumerator DestructVFX(GameObject vfx)
    {
        yield return new WaitForSeconds(1f);
        Destroy(vfx);
    }
}
