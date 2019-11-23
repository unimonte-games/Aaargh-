using Photon.Pun;
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
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public float vida = 100f;
    bool chamouMorte = false;

    public AudioClip vaca;

    AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player").transform;
        player3 = GameObject.FindGameObjectWithTag("Player").transform;
        player4 = GameObject.FindGameObjectWithTag("Player").transform;
        pperto = false;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //movimentação
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
        if (Vector3.Distance(transform.position, player2.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player2.position, speed * Time.deltaTime);
            pperto = true;
        }
        else if (Vector3.Distance(transform.position, player2.position) < stoppingDistance && Vector3.Distance(transform.position, player2.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            pperto = false;
        }
        else if (Vector3.Distance(transform.position, player2.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player2.position, -speed * Time.deltaTime);
            pperto = false;
        }
        if (Vector3.Distance(transform.position, player3.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player3.position, speed * Time.deltaTime);
            pperto = true;
        }
        else if (Vector3.Distance(transform.position, player3.position) < stoppingDistance && Vector3.Distance(transform.position, player3.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            pperto = false;
        }
        else if (Vector3.Distance(transform.position, player3.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player3.position, -speed * Time.deltaTime);
            pperto = false;
        }
        if (Vector3.Distance(transform.position, player4.position) < stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player4.position, speed * Time.deltaTime);
            pperto = true;
        }
        else if (Vector3.Distance(transform.position, player4.position) < stoppingDistance && Vector3.Distance(transform.position, player4.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            pperto = false;
        }
        else if (Vector3.Distance(transform.position, player4.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player4.position, -speed * Time.deltaTime);
            pperto = false;
        }
        //faz atirar quando esta perto
        if (pperto == true)
        {
            if (timeShots <= 0)
            {
                PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);
                timeShots = StartTimeShots;
            }
            else
            {
                timeShots -= Time.deltaTime;
                pperto = false;
            }
        }
        //diz que quando chegar em 0 chama a função morte
        if (vida <= 0)
        {
            vida = 0;
            if (chamouMorte == false)
            {
                chamouMorte = true;
                StartCoroutine("Morrer");
                //AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                //AudioSource.PlayClipAtPoint(vaca, transform.position);
            }
        }
    }

    IEnumerator Morrer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);    
            Destroy(gameObject);
        PhotonNetwork.LoadLevel(9);
    }
}
