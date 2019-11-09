using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabiInvo : MonoBehaviourPun, IPunObservable
{
    public GameObject shield;
    public GameObject golem;
    public Transform PosiInstantiate;
    public float cooldownTime = 1;

    private float nextFireTime = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PhotonNetwork.Instantiate("Shield", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PhotonNetwork.Instantiate("Golem", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                }
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
