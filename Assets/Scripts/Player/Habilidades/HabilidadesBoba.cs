using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBoba : MonoBehaviour
{
    public int damage;
    public float range;
    public GameObject escopeta;
    public GameObject bullet;

    public GameObject prefab;
    void FixedUpdate()
    {
        //Caixa
        if (Input.GetKeyUp(KeyCode.R))
        {
            Tiro();
            Debug.Log("Deu tiro");
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Arma();
            Debug.Log("Deu tiro");
        }
        
    }

    void Arma()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);

    }
    void Tiro()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            hit.transform.gameObject.GetComponent<IA>().vida = -10;
        }
    }
    
}
