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
    [SerializeField] private float _boomerangVelocityThreshold; // Permet d'ajuster le point à partir duquel revient le boomerang lorsqu'il a été lancé
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
                if (collision.gameObject == _owner) // Si l'objet de la collision est le joueur qui a lancé le boomerang, ce dernier le reprend comme parent et reset sa vélocité
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

    public IEnumerator Shoot()
    {
        this.transform.parent = null; // Détache le boomerang de son parent le joueur
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.x > _boomerangVelocityThreshold); // Attend que la vélocité ait diminuée
        
        if(!_hasToFall) // Si le boomerang ne doit pas tomber, autrement dit s'il n'a pas été stopper par un obstacle, on le fait revenir vers sa position initale
        {
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        }
    }
}

