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
    public GameObject player;
    public float vida = 100f;
    bool chamouMorte = false;

    public AudioClip vaca;

    AudioSource audioSource;

    void Start()
    {
        pperto = true;
        audioSource = GetComponent<AudioSource>();
    }
    GameObject PlayerPerto()
    {
        GameObject[] PPP = GameObject.FindGameObjectsWithTag("Player");
        float Dist = float.MaxValue;
        GameObject alvo = null;
        for (int i = 0; i < PPP.Length; i++)
        {
            if ((PPP[i].transform.position - transform.position).magnitude < Dist)
            {
                alvo = PPP[1];
                Dist = (PPP[i].transform.position - transform.position).magnitude;
            }
        }
        return alvo;
    }
    void Update()
    {
        player = PlayerPerto();
        //movimentação
        if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            pperto = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < stoppingDistance && Vector3.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            pperto = false;
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            pperto = false;
        }
        //faz atirar quando esta perto
        if (pperto == true)
        {
            if (timeShots <= 0)
            {
                PhotonNetwork.Instantiate("Bullet", transform.position, transform.rotation);
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bala")
        {
            vida -= 10;
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
