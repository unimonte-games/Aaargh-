using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativador : MonoBehaviour
{
    public GameObject barco;
    public GameObject PrimeiroPergaminho;
    public GameObject SegundoPergaminho;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            barco.SetActive(true);
            PrimeiroPergaminho.SetActive(false);
            SegundoPergaminho.SetActive(true);
        }
        if(collision.gameObject.name == "navio")
        {
            PhotonNetwork.LoadLevel(5);
        }
    }
}
