using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBColisor : MonoBehaviour
{
    //Colisor dos Objetos
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Inimigo")
        {
            IA p = collision.gameObject.GetComponent<IA>();
            p.vida -= 10;
        }
        else if (collision.gameObject.tag == "Inimigo2")
        {
            IAPRAGA p = collision.gameObject.GetComponent<IAPRAGA>();
            p.vida -= 10;
        }
        Destroy(gameObject);
    }
}
