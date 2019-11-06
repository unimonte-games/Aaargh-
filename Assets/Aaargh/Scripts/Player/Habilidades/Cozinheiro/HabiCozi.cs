using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabiCozi : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] objetos;
    public Transform PosiInstantiate;
    public int objN;
    public int objC = 0;
    public float cooldownTime = 1;

    private float nextFireTime = 0;
    void Update()
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
            }
            //Colocar script OBCOLISOR nos objetos para funcionar a colisao
            if (Input.GetKeyDown(KeyCode.R))
            {
                IRandom();
                nextFireTime = Time.time + cooldownTime;
            }
        }
            
        
         
    }
    //da os Buffs
    void BUFF(GameObject GBJ)
    {
        GBJ.GetComponent<Player>().velocidade += 10f;
        GBJ.GetComponent<Player>().vida -= 9;
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
        objetos[objC] = Instantiate(objetos[objC], PosiInstantiate.position + PosiInstantiate.forward, PosiInstantiate.rotation);
        Destroy(objetos[objC], 0.3f);
    }
    
}
