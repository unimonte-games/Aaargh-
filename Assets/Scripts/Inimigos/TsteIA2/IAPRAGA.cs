using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPRAGA : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public float vida = 100;
    bool chamouMorte = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            
        }
        if (vida <= 0)
        {
            vida = 0;
            if (chamouMorte == false)
            {
                chamouMorte = true;
                StartCoroutine("Morrer");
            }
        }
    }
    IEnumerator Morrer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }
}
