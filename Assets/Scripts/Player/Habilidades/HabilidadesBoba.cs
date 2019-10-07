using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBoba : MonoBehaviour
{
    public float lookRadius = 10f;
    public GameObject prefab;

    public GameObject bullet;
    public float timeShots;
    public float StartTimeShots;
    void Update()
    {
        Distract();
        Arma();
    }

    void Distract()
    {
        if (Input.GetButtonDown("E"))
        {
            Instantiate(prefab);
            prefab.AddComponent(typeof(Rigidbody));
            prefab.AddComponent<Rigidbody>().isKinematic = true;
        }
    }
    void Arma()
    {
        if (Input.GetMouseButton(0))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeShots = StartTimeShots;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
