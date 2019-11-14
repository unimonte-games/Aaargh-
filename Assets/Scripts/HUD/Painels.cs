using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painels : MonoBehaviour
{
    public GameObject painels;
    private void OnCollisionEnter(Collision collision)
    {
        painels.SetActive(true);
    }
    private void OnCollisionExit(Collision collision)
    {
        painels.SetActive(false);
    }
    
}
