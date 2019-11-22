using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine("Morrer");
    }
    IEnumerator Morrer()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
