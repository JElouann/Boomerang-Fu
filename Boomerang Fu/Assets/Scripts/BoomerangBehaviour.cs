using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BoomerangBehaviour : MonoBehaviour
{
    [SerializeField] private float _forceValue;
    private Rigidbody _rb;
    private bool _hasToFall;
    private PlayerInputHandler _input;
    [SerializeField] private float _boomerangVelocityThreshold; // Permet d'ajuster le point � partir duquel revient le boomerang lorsqu'il a �t� lanc�
    private GameObject _owner;
    
    private void Awake()
    {
        _owner = this.gameObject.transform.parent.gameObject;
        _rb = GetComponent<Rigidbody>();
    }

    public void StartAction(InputAction.CallbackContext value)
    {
        StartCoroutine(Shoot());
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject == _owner) // Si l'objet de la collision est le joueur qui a lanc� le boomerang, ce dernier le reprend comme parent et reset sa v�locit�
                {
                    this.transform.parent = collision.gameObject.transform;
                    _rb.velocity = Vector3.zero;
                    collision.gameObject.SendMessage("AttachBoomerang", this);
                }
                break;
            default:
                _hasToFall = true;
                StopCoroutine(Shoot());
                _rb.velocity = Vector3.zero;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) // [A VERIFIER]
    {
        if (other.gameObject.tag == "Player")
        {
            this.transform.parent = other.gameObject.transform;
            HasAParent = true;
        }
    }

    public IEnumerator Shoot()
    {
        this.transform.parent = null; // D�tache le boomerang de son parent le joueur
        HasAParent = false;
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.x > _boomerangVelocityThreshold); // Attend que la v�locit� ait diminu�e
        
        if(!_hasToFall) // Si le boomerang ne doit pas tomber, autrement dit s'il n'a pas �t� stopper par un obstacle, on le fait revenir vers sa position initale
        {
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        }
    }
}

