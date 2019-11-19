using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painels : MonoBehaviour
{
    public GameObject painels;
    public GameObject fechar;
    public GameObject barco;
    public void Abrir()
    {
        painels.SetActive(true);
        fechar.SetActive(true);
    }
    public void Fechar()
    {
        painels.SetActive(false);
        fechar.SetActive(false);
    }

    

}
