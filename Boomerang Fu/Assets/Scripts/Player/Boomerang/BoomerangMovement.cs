using System.Collections;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    // Boomerang movement variable
    [SerializeField]
    private float _forceValue;
    [SerializeField]
    private float _boomerangVelocityThreshold;

    // Components useful for this script
    private BoomerangVFX _vfx;
    private AudioSource _source;
    private Rigidbody _rb;

    public void StartAction()
    {
        // Start the coroutine to shoot
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        // Play audio and VFX
        _source.Play();
        _vfx.Enable();

        // Enable gravity and collision
        _rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;

        // Detach the boomerang from the player
        transform.parent = null;

        // Add force to the vector forward of the boomerang
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse);

        // Wait until the velocity is low enough
        do
        {
            yield return new WaitForFixedUpdate();
        }
        while (_rb.velocity.magnitude > _boomerangVelocityThreshold);

        // Turn the VFX
        _vfx.Turn();

        // Reset the velocity
        _rb.velocity = Vector3.zero;

        // Then add force backwards
        _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
    }

    private void Awake()
    {
        // Get all components
        _rb = GetComponent<Rigidbody>();
        _source = GetComponent<AudioSource>();
        _vfx = GetComponent<BoomerangVFX>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // If collision -> stop coroutine (a.k.a. movement)
        // And reset velocity
        StopAllCoroutines();
        _rb.velocity = Vector3.zero;
    }
}
