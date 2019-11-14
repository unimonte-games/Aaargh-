using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class HabiCozi : MonoBehaviourPun, IPunObservable
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] objetos;
    public Transform PosiInstantiate;
    public int objN;
    public int objC = 0;
    public float cooldownTime = 1;
    public Image imageCooldown;
    public Image imageCooldown2;

    private float nextFireTime = 0;
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        BUFF(players[i]);
                    }
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.Cozinheiro1);

                }
                //Colocar script OBCOLISOR nos objetos para funcionar a colisao
                if (Input.GetKeyDown(KeyCode.R))
                {
                    IRandom();
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown2.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.Cozinheiro2);

                }
            }
        }
    }
    //da os Buffs
    void BUFF(GameObject GBJ)
    {
        GBJ.GetComponent<Player2>().vida += 9;
        GBJ.GetComponent<Player2>().healthBar.fillAmount += 9;
    }
    //Randomizacao dos itens 
    void IRandom()
    {
        objN = Random.Range(0, 3);
        objC = 0;
        while(objC < 3)
        {
            objetos[objC].SetActive(false);
            objC += 1;
        }
        objetos[objN].SetActive(true);
        objetos[objC] = PhotonNetwork.Instantiate(objetos[objC].name, PosiInstantiate.position + PosiInstantiate.forward, PosiInstantiate.rotation);
        Destroy(objetos[objC], 0.3f);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }

}
