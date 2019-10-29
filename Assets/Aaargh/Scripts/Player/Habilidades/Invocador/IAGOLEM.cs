using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGOLEM : MonoBehaviour
{
    public float speed;
    public int vida = 100;
    public Rigidbody rgbGolem;
    public Transform PosiInstantiate;
    bool chamouMorte = false;

    private Vector3 target;
    private Transform player;

    private void Start()
    {
        rgbGolem.AddForce(transform.forward * 20, ForceMode.Impulse);
    }
    void Update()
    {
        PosiInstantiate.position = Vector3.MoveTowards(PosiInstantiate.position, PosiInstantiate.position, speed * Time.deltaTime);
        
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
        if(collision.gameObject.tag == "Inimigo")
        {
            StartCoroutine("Morrer");
            IA p = collision.gameObject.GetComponent<IA>();
            p.vida -= 100;
        }
        if(collision.gameObject.tag == "Inimigo")
        {
            IAPRAGA p = collision.gameObject.GetComponent<IAPRAGA>();
            p.vida -= 10;
        }
        else
        {
            StartCoroutine("PerS");

        }
    }
    IEnumerator Morrer()
    {
            GetComponent<MeshRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }
    IEnumerator PerS()
    {
        vida -= 10;
        yield return new WaitForSeconds(1);
        vida -= 10;
        yield return new WaitForSeconds(1);
        vida -= 10;
    }
}
