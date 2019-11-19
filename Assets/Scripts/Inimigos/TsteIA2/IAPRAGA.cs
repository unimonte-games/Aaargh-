using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPRAGA : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform[] player;
    public Transform[] alvo;
    public float vida = 100f;
    bool chamouMorte = false;
    void Start()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = GameObject.FindGameObjectWithTag("Player").transform;

        }

    }
    void Update()
    {
        for (int i = 0; i < alvo.Length; i++)
        {
            //Focar caixa ou focar o player
            if (GameObject.Find("Caixa"))
            {
                alvo[i] = GameObject.Find("Caixa").transform;
            }
            else
            {
                for (int j = 0; j < player.Length; j++)
                {
                    alvo[i] = player[j];
                }
            }
            if (Vector3.Distance(transform.position, alvo[i].position) < stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, alvo[i].position, speed * Time.deltaTime);

            }
            else if (Vector3.Distance(transform.position, alvo[i].position) < stoppingDistance && Vector3.Distance(transform.position, alvo[i].position) > retreatDistance)
            {
                transform.position = this.transform.position;

            }
            else if (Vector3.Distance(transform.position, alvo[i].position) < retreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, alvo[i].position, -speed * Time.deltaTime);

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
