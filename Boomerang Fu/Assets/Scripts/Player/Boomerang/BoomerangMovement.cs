using System.Collections;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
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

        this.transform.parent = null;
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse);

        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.magnitude > _boomerangVelocityThreshold);      

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

