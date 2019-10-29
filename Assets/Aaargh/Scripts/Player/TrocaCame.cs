using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaCame : MonoBehaviour
{
    public GameObject barco;
    public GameObject cameradbarco;
    public GameObject player;
    public GameObject pdnotimao;
    public GameObject cameraplayer;
    public bool ontimao;

    public void Start()
    {
        ontimao = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Timao")
        {
            //primeira pessoa
            barco.GetComponent<Rigidbody>().isKinematic = false;
            barco.GetComponent<WaterBoat>().enabled = true;
            cameradbarco.SetActive(true);
            player.SetActive(false);
            cameraplayer.SetActive(false);
            TimaoOn();

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Parrudinho")
        {
            //terceira pessoa
            barco.GetComponent<Rigidbody>().isKinematic = true;
            barco.GetComponent<WaterBoat>().enabled = false;
            cameradbarco.SetActive(false);
            player.SetActive(true);
            player.transform.position = pdnotimao.transform.position;
            cameraplayer.SetActive(true);
            ontimao = false;
        }
    }
    public void TimaoOn()
    {
        ontimao = true;
    }
}
