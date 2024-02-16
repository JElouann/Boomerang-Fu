using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private GameObject _dashVFX;
    [SerializeField] private AudioSource _src;

    [SerializeField] private float _vitesseDash;
    private Rigidbody _rb;

    [HideInInspector] public float _timer = 0;
    public float CoolDown;

    private void Awake()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>();
        _input.OnDash += Dash;

        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
    }

    void Dash(InputAction.CallbackContext value)
    {
        if (value.performed && _timer < 0)
        {
            // Set timer
            _timer = CoolDown;
            
            // Add Force
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(Vector3.forward * _vitesseDash * 5, ForceMode.Impulse);

            // Play Sound
            _src.Play();

            // VFX
            GameObject _dashVFXClone = Instantiate(_dashVFX, transform);
            _dashVFXClone.transform.parent = null;
            StartCoroutine(DestructVFX(_dashVFXClone));
        }
    }

    private IEnumerator DestructVFX(GameObject vfx)
    {
        yield return new WaitForSeconds(1f);
        Destroy(vfx);
    }
}
