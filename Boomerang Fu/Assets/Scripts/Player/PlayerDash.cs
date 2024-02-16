using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    // Dash timing variable
    [HideInInspector]
    public float Timer = 0;
    public float CoolDown;
    
    private Rigidbody _rb;

    [SerializeField]
    private GameObject _dashVFX;
    [SerializeField]
    private AudioSource _src;
    [SerializeField]
    private float _vitesseDash;

    private void Awake()
    {
        // Bind Dash input.
        var input = GetComponentInChildren<PlayerInputHandler>();
        input.OnDash += Dash;

        // Get Rigidbody to add force
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Decrease the timer by the time that has passed since last time.
        Timer -= Time.fixedDeltaTime;
    }

    private void Dash(InputAction.CallbackContext value)
    {
        // If dash is perfomed
        if (value.performed && Timer < 0)
        {
            // Set timer
            Timer = CoolDown;

            // Add Force
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(Vector3.forward * _vitesseDash * 5, ForceMode.Impulse);

            // Play Sound
            _src.Play();

            // VFX
            GameObject dashVFXClone = Instantiate(_dashVFX, transform);
            dashVFXClone.transform.parent = null;
            StartCoroutine(DestructVFX(dashVFXClone));
        }
    }

    private IEnumerator DestructVFX(GameObject vfx)
    {
        // Wait 1s then destroy the GameObject
        // 1s is plenty of time for the VFX to finish
        yield return new WaitForSeconds(1f);
        Destroy(vfx);
    }
}
