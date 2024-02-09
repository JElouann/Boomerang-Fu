using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BoomerangBehaviour : MonoBehaviour
{
    public bool isOrphan;
    [SerializeField] private float _forceValue;
    private Rigidbody _rb;
    private bool _hasToFall;
    private PlayerInputHandler _input;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void StartAction(InputAction.CallbackContext value)
    {
        StartCoroutine(Shoot());   
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si l'objet de la collision n'est pas un joueur, le boomerang tombe
        if (!collision.gameObject.CompareTag("Player"))
        {
            _hasToFall = true;
            StopCoroutine(Shoot());
            _rb.velocity = Vector3.zero;
        }
    }

    public IEnumerator Shoot()
    {
        this.transform.parent = null; // Détache le boomerang de son parent le joueur
        _rb.AddRelativeForce(_forceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.velocity.x > 0.1f); // Attend que la vélocité ait diminuée
        
        if(!_hasToFall)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddRelativeForce(-_forceValue, 0, 0, ForceMode.Impulse);
        }
    }
}

