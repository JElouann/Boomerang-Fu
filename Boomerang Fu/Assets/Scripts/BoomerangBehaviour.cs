using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.Windows;

public class BoomerangBehaviour : MonoBehaviour
{
    [SerializeField] private float _forceValue;
    private Rigidbody _rb;
    private bool _hasToFall;
    private PlayerInputHandler _input;
    [SerializeField] private float _boomerangVelocityThreshold; // Permet d'ajuster le point � partir duquel revient le boomerang lorsqu'il a �t� lanc�
    private GameObject _owner;
    private PlayerMain _ownerPlayerMain;

    private Vector3 _localPosition;
    private Quaternion _localRotation;
    private Vector3 _localScale;

    private Vector3 _aimLocation;

    [SerializeField] private GameObject _trail;
    [SerializeField] private GameObject _deathVFXPrefab;

    private void Awake()
    {
        _owner = this.gameObject.transform.parent.gameObject;
        _ownerPlayerMain = _owner.GetComponent<PlayerMain>();
        _rb = GetComponent<Rigidbody>();

        _localPosition = this.transform.localPosition;
        _localRotation = this.transform.localRotation;
        _localScale = this.transform.localScale;

        this.GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
    }

    public void StartAction(Vector2 location)
    {
        _aimLocation = location;
        StartCoroutine(Shoot());
    }

    private void FixedUpdate()
    {
        if(_owner == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Attach(GameObject parent)
    {
        this.transform.parent = parent.transform;
        this.transform.localPosition = _localPosition;
        this.transform.localRotation = _localRotation;
        this.transform.localScale = _localScale;

        this.GetComponent<Collider>().enabled = false;

        _rb.velocity = Vector3.zero;

        _rb.isKinematic = true;

        parent.SendMessage("AttachBoomerang", this);
        this.StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.tag);
        _trail.SetActive(false);
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject == _owner) // Si l'objet de la collision est le joueur qui a lanc� le boomerang, ce dernier le reprend comme parent et reset sa v�locit�
                {
                    if(_owner.GetComponent<PlayerAttack>()._boomerangBehaviour == null) Attach(collision.gameObject);
                }
                else
                {
                    if(_rb.velocity.magnitude < 0.3 && collision.gameObject.GetComponent<PlayerAttack>()._boomerangBehaviour == null)
                    {
                        _owner = collision.gameObject;
                        Attach(collision.gameObject);
                    }
                    else
                    {
                        GameManager.Instance.Score[_ownerPlayerMain.id]++;
                        Debug.Log(GameManager.Instance.Score[_ownerPlayerMain.id]);
                        GameObject death = Instantiate(_deathVFXPrefab, collision.gameObject.transform);
                        death.transform.parent = null;
                        print($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH {death}");
                        Destroy(collision.gameObject);
                    }
                }
                break;
            default:
                GetComponent<Renderer>().materials[0].SetColor("_Color", GetComponent<Renderer>().materials[0].color * 0.5f);
                this.StopAllCoroutines();
                _rb.velocity = Vector3.zero;
                break;
        }
    }

    public IEnumerator Shoot()
    {
        _trail.SetActive(true);
        _trail.transform.rotation = gameObject.transform.rotation;

        _rb.isKinematic = false;
        this.GetComponent<Collider>().enabled = true;
         
        this.transform.parent = null; // Détache le boomerang de son parent le joueur
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            print($"aaaaa {_rb.velocity.magnitude}");
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.magnitude > _boomerangVelocityThreshold); // Attend que la vélocité ait diminuée

        _trail.SetActive(false);
        _trail.transform.localRotation = new Quaternion(0, 180, 0, 0);
        print("Je retourne sur ma planete");


        _rb.velocity = Vector3.zero;
        _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        _trail.SetActive(true);
    }
}

