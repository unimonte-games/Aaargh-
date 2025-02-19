﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2 : MonoBehaviourPun, IPunObservable
{
    public float velocidade;
    public PhotonView pv;

    //Otimização :3 (evita coleta de lixo desnecessário)
    Vector3 deltaMovimento = Vector3.zero;
    public Transform raioRef;
    public float raioDist, chaoDist;
    Transform meu_transform;
    public int vida = 100;
    public GameObject[] inimigo;
    public Animator anim;
    public float jumpforce = 5f;
    public Rigidbody rb;

    [Header("Unity Stuff")]
    public Image healthBar;

    public bool IsGrounded;

    //Audio :o
    public AudioClip andar;
    public AudioClip hitnoplayer;
    AudioSource audioSource;

    void Awake()
    {
        if (photonView.IsMine)
        {
            meu_transform = GetComponent<Transform>();
            transform.tag = "Player";
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            pv = GetComponent<PhotonView>();
        }
    }
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // guarda de antemão a rotação

            // aplica movimento
            {
                // reseta rotação para que a função Translate não nos faça voar

                

                // define eixos usando entrada do usuário
                deltaMovimento.x = Input.GetAxis("Horizontal");
                deltaMovimento.z = Input.GetAxis("Vertical");

                // normaliza se necessário
                if (deltaMovimento.magnitude > 1)
                    deltaMovimento.Normalize();

                // multiplica pela velocidade e delta tempo
                deltaMovimento *= velocidade * Time.deltaTime;
                // aplica movimento
                meu_transform.Translate(deltaMovimento);

                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
                {
                    Jump();
                }
                if (Input.GetButtonDown("Horizontal"))
                {
                    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(andar, transform.position);
                    Moviment();
                }
                if (Input.GetButtonDown("Vertical"))
                {
                    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(andar, transform.position);
                    Moviment();
                }
            }
            // acompanhar chão
            {
                // faz raycast pra baixo
                Ray raio = new Ray(raioRef.position, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(raio, out hit, raioDist))
                {
                    // acompanha distância do chão
                    Vector3 pos = meu_transform.position;
                    pos.y = hit.point.y + chaoDist;
                    meu_transform.position = pos;
                }
            }
            // redefine a rotação que tinha antes

            if (vida <= 0)
            {
                vida = 0;
                Morto();
            }
        }
    }

    void Morto()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LoadLevel(4);//Fazer cena para morto
            vida += 100;
            anim.SetBool("Death", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {

            if (collision.gameObject.tag == "Inimigo")
            {
                vida -= 10;
                healthBar.fillAmount = vida / 100;
                AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(hitnoplayer, transform.position);
            }
            if (collision.gameObject.tag == "Ground")
            {
                IsGrounded = true;
            }
            if(collision.gameObject.name == "CasaFeiticeiro")
            {
                PhotonNetwork.LoadLevel(5);
            }
            if(collision.gameObject.tag == "MudaScene")
            {
                PhotonNetwork.LoadLevel(4);
            }
            if(collision.gameObject.name == "navio")
            {
                PhotonNetwork.LoadLevel(8);
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Ground")
            {
                IsGrounded = false;
            }
        }
    }
    private void Moviment()
    {
        anim.SetBool("Andando", true);

        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        //Vector3 moviment = new Vector3(x, y);
        //Debug.Log("Funfando");
        //if (moviment == Vector3.zero)
        //{
        //    anim.SetBool("Andando", true);
        //    anim.SetFloat("Ver", y);
        //    anim.SetFloat("Hor", x);
        //}
        //else
        //{
        //    anim.SetBool("Andando", false);
        //}

    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpforce);
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // se não se esqueceu de referenciar a referencia do raio
        if (raioRef)
        {
            // cria raio e hit
            Ray raio = new Ray(raioRef.position, Vector3.down);
            RaycastHit hit;

            // faz raycast (é um raio (linha) matemática que colide em colliders, o resultado é passado pra hit)
            if (Physics.Raycast(raio, out hit, raioDist))
            {
                // se fez contato, troca a cor de Gizmos pra verde
                // (Gizmos são as linhas exclusivas do editor)
                Gizmos.color = Color.green;

                // desenha ponto de colisão do raycast disparado (isso pega a superfície colidida do collider)
                Gizmos.DrawWireCube(hit.point, Vector3.one * 0.1f);

                // pega posição do próprio transform e
                // define a altura da posição como a altura do ponto colidido do raycast + distância do chão
                Vector3 pos = transform.position;
                pos.y = hit.point.y + chaoDist;

                // desenha essa posição calculada
                Gizmos.DrawWireSphere(pos, 0.1f);
            }
            else // senão colidiu em nada, então desenha Gizmos vermelhos (só a linha vai ser desenhada)
            {
                Gizmos.color = Color.red;
            }

            // desenha linha representando Raycast
            Gizmos.DrawLine(raioRef.position, raioRef.position + Vector3.down * raioDist);
        }
    }

#endif

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
