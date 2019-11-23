using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoAtk : MonoBehaviour
{
    public GameObject player;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public bool Atack;

    public AudioClip praga;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Atack = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Atacar();
        }
    }

    void Atacar()
    {
        if (Atack == true && player)
        {
            StartCoroutine("TempoDAtaque");
                player.GetComponent<Player2>().vida -= 10;
            AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(praga, transform.position);
        }
        if (Atack == true && player2)
        {
            StartCoroutine("TempoDAtaque");
            AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(praga, transform.position);
        }
        if (Atack == true && player3)
        {
            StartCoroutine("TempoDAtaque");
            AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(praga, transform.position);
        }
        if (Atack == true && player4)
        {
            StartCoroutine("TempoDAtaque");
            AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(praga, transform.position);
        }
    }
    IEnumerator TempoDAtaque()
    {
        Atack = false;
        yield return new WaitForSeconds(1);
        Atack = true;
    }
}
