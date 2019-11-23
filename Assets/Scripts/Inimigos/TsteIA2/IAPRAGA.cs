using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPRAGA : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public GameObject[] player;
    public Transform alvo;
    public float vida = 100f;
    bool chamouMorte = false;

    public AudioClip hitnoinimigo;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    GameObject PlayerPerto()
    {
        GameObject[] PPP = GameObject.FindGameObjectsWithTag("Player");
        float Dist = float.MaxValue;
        GameObject alvo = null;
        for (int i = 0; i < PPP.Length; i++)
        {
           if((PPP[i].transform.position - transform.position).magnitude < Dist)
            {
                alvo = PPP[1];
                Dist = (PPP[i].transform.position - transform.position).magnitude;
            }
        }
        return alvo;
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
            alvo = PlayerPerto().transform;
        }
        if (Vector3.Distance(transform.position, alvo.position) > stoppingDistance)
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
            //AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            //AudioSource.PlayClipAtPoint(hitnoinimigo, transform.position);
        }
        if(collision.gameObject.name == "Bala")
        {
            vida -= 10;
        }
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<Player2>().vida -= 10;
        }
    }
    IEnumerator Morrer()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }
}
