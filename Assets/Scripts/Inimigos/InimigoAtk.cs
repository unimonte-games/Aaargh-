using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoAtk : MonoBehaviour
{
    public GameObject player;

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
        if (Atack == true)
        {
            StartCoroutine("TempoDAtaque");
            player.GetComponent<Player>().vida -= 10;
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
