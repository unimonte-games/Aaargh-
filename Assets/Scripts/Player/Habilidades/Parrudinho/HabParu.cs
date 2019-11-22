using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using UnityEngine.UI;

public class HabParu : MonoBehaviourPun, IPunObservable
{
    public List<GameObject> Inimigos = new List<GameObject>();

    public float cooldownTime = 1;
    public Image imageCooldown;
    public Animator anim;
    public Image imageCooldown2;

    private float nextFireTime = 0;
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetBool("Skill", true);
                    for (int i = 0; i < Inimigos.Count; i++)
                    {
                        PalmaDMorte(Inimigos[i]);
                    }
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.ExMarinheiro2);
                    anim.SetBool("Skill", false);

                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    anim.SetBool("SkillII", true);
                    for (int i = 0; i < Inimigos.Count; i++)
                    {
                        Contadordevida(Inimigos[i]);
                        Vector3 posicao = new Vector3(Inimigos[i].transform.position.x, Inimigos[i].transform.position.y, Inimigos[i].transform.position.z - 10);
                        Inimigos[i].transform.position = posicao;
                    }
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown2.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.ExMarinheiro1);
                    anim.SetBool("SkillII", false);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Inimigo")
            Inimigos.Add(collision.transform.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Inimigo")
            Inimigos.Remove(collision.gameObject);
    }
    void Contadordevida(GameObject gbj)
    {
        if (gbj.GetComponent<IAPRAGA>())
            gbj.GetComponent<IAPRAGA>().vida -= 15f;

        if (gbj.GetComponent<IA>())
            gbj.GetComponent<IA>().vida -= 15f;
    }
    void PalmaDMorte(GameObject GBJ)
    {
        if (GBJ.GetComponent<IAPRAGA>())
            GBJ.GetComponent<IAPRAGA>().vida -= 100f;

        if (GBJ.GetComponent<IA>())
            GBJ.GetComponent<IA>().vida -= 100f;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
