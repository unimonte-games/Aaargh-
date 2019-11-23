using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilidadesBoba : MonoBehaviourPun, IPunObservable
{
    public float cooldownTime = 1;
    public GameObject prefab;
    public Transform PosiInstantiate;
    private float nextFireTime = 0;
    public Image imageCooldown;
    public Animator anim;
    public AudioClip boba2;

    AudioSource audioSource;

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    //anim.SetBool("Skill", true);
                    Caixa();
                    nextFireTime = Time.time + cooldownTime;
                    anim.SetBool("Skill", true);
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    anim.SetBool("Skill", false);
                    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(boba2, transform.position);
                    //StartCoroutine("Morrer");
                    //anim.SetBool("Skill", true);
                }
            }
        }
            
    }

    void Caixa()
    {
        PhotonNetwork.Instantiate("Caixa", PosiInstantiate.position, PosiInstantiate.rotation);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
