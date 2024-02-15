using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SearchService;
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
    private PlayerMain _ownerPlayerMain;

    private GameObject clone; //

    private void Awake()
    {
        _owner = this.gameObject.transform.parent.gameObject;
        _ownerPlayerMain = this._owner.GetComponent<PlayerMain>();
        _rb = GetComponent<Rigidbody>();
    }

    public void StartAction(InputAction.CallbackContext value)
    {
        GameObject clone = Instantiate(gameObject); //
        //StartCoroutine(Shoot());
    }

    private void FixedUpdate() //
    {
        if (this.transform.parent != null)
        {
            _rb.velocity = Vector3.zero;
        }
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
                    break;
                }
                else
                {
                    GameManager.Instance.Score[_ownerPlayerMain.id]++;
                    Debug.Log(GameManager.Instance.Score[_ownerPlayerMain.id]);
                    break;
                }
            default:
                _hasToFall = true;
                StopCoroutine(Shoot());
                _rb.velocity = Vector3.zero;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) // [A VERIFIER] -> récupérer le boomerang
    {
        if (other.gameObject.tag == "Player")
        {
            this.transform.parent = other.gameObject.transform;
            this.transform.rotation = Quaternion.Euler(new Vector3(other.gameObject.transform.rotation.x, other.gameObject.transform.rotation.y , other.gameObject.transform.rotation.z)); //
            _rb.constraints = RigidbodyConstraints.FreezePosition; //
        }
    }

    public IEnumerator Shoot()
    {
        /*
        this.transform.parent = null; // Détache le boomerang de son parent le joueur
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; // Corrige les bugs occasionnés par les physiques
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.x > _boomerangVelocityThreshold); // Attend que la vélocité ait diminuée

        if (!_hasToFall) // Si le boomerang ne doit pas tomber, autrement dit s'il n'a pas été stoppé par un obstacle, on le fait revenir vers sa position initale
        {
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        } */

        
        
        this.gameObject.SetActive(false);
        Rigidbody _cloneRb = clone.GetComponent<Rigidbody>();

        _cloneRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY; // Corrige les bugs occasionnés par les physiques
        _cloneRb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_cloneRb.velocity.x > _boomerangVelocityThreshold); // Attend que la vélocité ait diminuée

        if (!_hasToFall) // Si le boomerang ne doit pas tomber, autrement dit s'il n'a pas été stoppé par un obstacle, on le fait revenir vers sa position initale
        {
            _cloneRb.velocity = Vector3.zero;
            _cloneRb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        }
    }
}

