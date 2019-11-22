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

    // Start is called before the first frame update
    void Start()
    {
        Atack = true;
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
            SoundManager.PlaySound(SoundManager.Sound.Praga);
        }
        if (Atack == true && player2)
        {
            StartCoroutine("TempoDAtaque");
            player.GetComponent<Player2>().vida -= 10;
            SoundManager.PlaySound(SoundManager.Sound.Praga);
        }
        if (Atack == true && player3)
        {
            StartCoroutine("TempoDAtaque");
            player.GetComponent<Player2>().vida -= 10;
            SoundManager.PlaySound(SoundManager.Sound.Praga);
        }
        if (Atack == true && player4)
        {
            StartCoroutine("TempoDAtaque");
            player.GetComponent<Player2>().vida -= 10;
            SoundManager.PlaySound(SoundManager.Sound.Praga);
        }
    }
    IEnumerator TempoDAtaque()
    {
        Atack = false;
        yield return new WaitForSeconds(1);
        Atack = true;
    }
}
