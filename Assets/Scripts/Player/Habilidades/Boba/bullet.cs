using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody corpo;
    // Use this for initialization
    void Start()
    {
        corpo.AddForce(transform.forward * 20, ForceMode.Impulse);
    }
    //bala player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player2 p = collision.gameObject.GetComponent<Player2>();
            p.vida -= 10;
        }
        else if (collision.gameObject.tag == "Inimigo")
        {
            IAPRAGA p = collision.gameObject.GetComponent<IAPRAGA>();
            p.vida -= 10 ;
        }
        Destroy(gameObject);
    }
}
