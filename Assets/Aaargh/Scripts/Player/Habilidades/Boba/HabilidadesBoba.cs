using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBoba : MonoBehaviour
{
    public float cooldownTime = 1;
    public GameObject prefab;
    public Transform PosiInstantiate;
    private float nextFireTime = 0;
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Caixa();
                Debug.Log("Deu tiro");
                nextFireTime = Time.time + cooldownTime;
            }
        }
            
    }

    void Caixa()
    {
        Instantiate(prefab, PosiInstantiate.position, Quaternion.identity);
    }
    
}
