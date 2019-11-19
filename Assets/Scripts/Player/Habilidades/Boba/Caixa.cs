using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    IEnumerator Morrer()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
