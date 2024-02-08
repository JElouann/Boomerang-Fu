using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Rigidbody Rb;

    public float DistanceTraget;
    private void Start()
    {
        Rb.AddForce(Vector3.forward * DistanceTraget , ForceMode.Impulse);
    }
}
