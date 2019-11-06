using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabiInvo : MonoBehaviour
{
    public GameObject shield;
    public GameObject golem;
    public Transform PosiInstantiate;
    public float cooldownTime = 1;

    private float nextFireTime = 0;
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFireTime)
        {
            //if (Input.GetKeyDown(KeyCode.E))
            //{
                //Instantiate(shield, PosiInstantiate.position, Quaternion.identity);
                //nextFireTime = Time.time + cooldownTime;
            //}
            if (Input.GetKeyDown(KeyCode.R))
            {
                PhotonNetwork.Instantiate("Golem", PosiInstantiate.position, Quaternion.identity);
                nextFireTime = Time.time + cooldownTime;
            }
        }

    }
}
