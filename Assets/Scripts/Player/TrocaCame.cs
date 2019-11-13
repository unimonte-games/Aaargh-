using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaCame : MonoBehaviour
{
    public GameObject barco;
    public GameObject cameradbarco;
    public GameObject[] player;
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
            cameraplayer.SetActive(false);
            TimaoOn();
            for (int i = 0; i < player.Length; i++)
            {
                player[i].SetActive(false);
                player[i].transform.position = pdnotimao.transform.position;
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //terceira pessoa
            barco.GetComponent<Rigidbody>().isKinematic = true;
            barco.GetComponent<WaterBoat>().enabled = false;
            cameradbarco.SetActive(false);
            cameraplayer.SetActive(true);
            ontimao = false;
            for (int i = 0; i < player.Length; i++)
            {
                player[i].SetActive(true);
                player[i].transform.position = pdnotimao.transform.position;
            }
        }
    }
    public void TimaoOn()
    {
        ontimao = true;
    }
}
