using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Rigidbody Rb;

    public float VitesseDash;

    private float timeduration = 0;

    void Update()
    {
        bool dash = Input.GetKey(KeyCode.Space);
        timeduration += 1 + timeduration;

        if (dash == true)
        {
            Rb.AddForce(Vector3.forward * VitesseDash, ForceMode.Impulse);

            if (timeduration >= 1)
            {
                
                timeduration = 0;
            }
        }
    }
}
