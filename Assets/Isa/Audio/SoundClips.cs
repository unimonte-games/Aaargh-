using UnityEngine;
using System.Collections;
using Photon.Pun;

public class SoundClips : MonoBehaviour
{
    //basicamente colocar isso nos scripts que chamam ações que precisam de som
    //Defaults
    public AudioClip arma;
    public AudioClip andar;
    public AudioClip hitnoplayer;
    public AudioClip hitnoinimigo;
    //Inimigos
    public AudioClip praga;
    public AudioClip galinha;
    public AudioClip vaca;
    //Habilidades
    public AudioClip Boba1;
    public AudioClip Boba2;
    public AudioClip Conj1;
    public AudioClip Conj2;
    public AudioClip Coz1;
    public AudioClip Coz2;
    public AudioClip Par1;
    public AudioClip Par2;
    
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PV = GetComponent<PhotonView>();
 
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //audioSource.PlayOneShot(arma, 0.7F);
        //    AudioSource.PlayClipAtPoint(arma, transform.position);
        //}
    }

    //To call the methods marked as PunRPC, you need a PhotonView component.Example call:

    PhotonView PV;

    public void Work()
    {
        PV.RPC("Sound", RpcTarget.AllBuffered);
    }
        
    [PunRPC]
    void Sound()
    {
        AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(arma, transform.position);
    }

    //exemplo
    //[PunRPC]
    //public void GunShot()
    //{
    //    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
    //    audioRPC.clip = gunSound;
    //    audioRPC.spatialBlend = 1;
    //    audioRPC.minDistance = 25;
    //    audioRPC.maxDistance = 100;
    //    audioRPC.Play();

    //}
}
