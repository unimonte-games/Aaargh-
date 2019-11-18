using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativador : MonoBehaviour
{
    public GameObject barco;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ativa")
        {
            barco.SetActive(true);
        }
        if(collision.gameObject.name == "navio")
        {
            PhotonNetwork.LoadLevel(5);
        }
    }
}
