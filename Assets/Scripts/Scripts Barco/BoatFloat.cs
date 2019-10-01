using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BoatFloat : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 2.0f;

    float forceFactor;
    Vector3 floatForce;

    void FixedUpdate()
    {
        forceFactor = 1.0f - (transform.position.y * waterLevel * floatThreshold * downForce);

        if(forceFactor > 1.0f)
        {
            floatForce = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
            GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
        }
    }

}
