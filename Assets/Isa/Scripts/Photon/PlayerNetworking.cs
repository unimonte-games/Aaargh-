using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviour
{
    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView PV;

    //Dentro do Player existe esse script; colocar script de movimentação dentro

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
    }
}
