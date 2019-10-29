using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float timeShots;
    public float StartTimeShots;
    public bool pperto;
    public GameObject bullet;
    public Transform player;
    public float vida = 100f;
    bool chamouMorte = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pperto = false;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            pperto = true;
        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            pperto = false;
        }
        else if(Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            pperto = false;
        }

        if (pperto == true)
        {
            if (timeShots <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                timeShots = StartTimeShots;
            }
            else
            {
                timeShots -= Time.deltaTime;
                pperto = false;
            }
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
