using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BoomerangBehaviour : MonoBehaviour
{
    [SerializeField]
    private float ForceValue;

    private Rigidbody rb;

    private bool _hasToFall;

    private PlayerInputHandler _input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            Debug.Log("WHAOUUUUUUU");
            rb.velocity = Vector3.zero;
        }
    }

    public IEnumerator Shoot()
    {
        this.transform.parent = null;
        rb.AddRelativeForce(ForceValue, 0, 0, ForceMode.Impulse); // Lance le boomerang droit devant
        do
        {
            yield return new WaitForFixedUpdate();
        } while (rb.velocity.x > 0.1f); // Attend que la vélocité ait diminuée
        
        if(!_hasToFall)
        {
            rb.velocity = Vector3.zero;
            rb.AddRelativeForce(-ForceValue, 0, 0, ForceMode.Impulse);
        }
    }
}

