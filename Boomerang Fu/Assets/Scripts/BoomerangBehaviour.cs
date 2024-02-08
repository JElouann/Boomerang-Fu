using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehaviour : MonoBehaviour
{
    [SerializeField]
    private float ForceValue;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }    
    }

    public IEnumerator Shoot()
    {
        rb.AddRelativeForce(ForceValue, 0, 0, ForceMode.Impulse);
        do
        {
            yield return new WaitForFixedUpdate();
        } while (rb.velocity.x > 0.1f);

        rb.velocity = Vector3.zero;
        rb.AddRelativeForce(-ForceValue, 0, 0, ForceMode.Impulse);
    }
}

