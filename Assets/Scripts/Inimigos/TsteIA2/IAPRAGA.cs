using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPRAGA : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public Transform alvo;
    public float vida = 100f;
    bool chamouMorte = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player").transform;
        player3 = GameObject.FindGameObjectWithTag("Player").transform;
        player4 = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Focar caixa ou focar o player
        if (GameObject.Find("Caixa"))
        {
           alvo = GameObject.Find("Caixa").transform;
        }
        else
        {
            alvo = player;
            alvo = player2;
            alvo = player3;
            alvo = player4;
        }
        if (Vector3.Distance(transform.position, alvo.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, alvo.position) < stoppingDistance && Vector3.Distance(transform.position, alvo.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, alvo.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, -speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, player2.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player2.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player2.position) < stoppingDistance && Vector3.Distance(transform.position, player2.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player2.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player2.position, -speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, player3.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player3.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player3.position) < stoppingDistance && Vector3.Distance(transform.position, player3.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player3.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player3.position, -speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, player4.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player4.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player4.position) < stoppingDistance && Vector3.Distance(transform.position, player4.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player4.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player4.position, -speed * Time.deltaTime);
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Shield")
        {
            StartCoroutine("Morrer");
        }
    }
    IEnumerator Morrer()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }
}
