using System.Collections;
using UnityEngine;
public class BoomerangMovement : MonoBehaviour
{
    // Boomerange Movement value
    [SerializeField] private float _forceValue;
    [SerializeField] private float _boomerangVelocityThreshold;

    private BoomerangVFX _vfx;
    private AudioSource _source;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _source = GetComponent<AudioSource>();
        _vfx = GetComponent<BoomerangVFX>();
    }

    public void StartAction()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        _source.Play();
        _vfx.Enable();

        _rb.isKinematic = false;
        this.GetComponent<Collider>().enabled = true;

        this.transform.parent = null; // Détache le boomerang de son parent le joueur
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.magnitude > _boomerangVelocityThreshold); // Attend que la vélocité ait diminuée        

        _vfx.Turn();
        _rb.velocity = Vector3.zero;
        _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collision)
    {
        this.StopAllCoroutines();
        _rb.velocity = Vector3.zero;   
    }
}

