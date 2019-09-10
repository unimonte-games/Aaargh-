using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
public class FloatObjects2 : MonoBehaviour
{
    public float waterLvl = 0.0f;
    public float floatTh = 2.0f;
    public float waDen = 0.125f;
    public float downF = 4.0f;

    float force;
    Vector3 floatF;

    // Update is called once per frame
    void FixedUpdate()
    {
        force = 1.0f - ((transform.position.y - waterLvl) / floatTh);

        if(force > 0.0f)
        {
            floatF = -Physics.gravity * GetComponent<Rigidbody>().mass * (force - GetComponent<Rigidbody>().velocity.y * waDen);
            floatF += new Vector3(0.0f, -downF, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatF, transform.position);
        }
    }
}
