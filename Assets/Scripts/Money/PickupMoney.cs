using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMoney : MonoBehaviour
{
    Money script;

    public int addAmount;

    void Start()
    {
        script = GameObject.FindWithTag("GameController").GetComponent<Money>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            script.gold += addAmount;
            Destroy(gameObject);
        }
    }
}
