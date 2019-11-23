using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabiInvo : MonoBehaviourPun, IPunObservable
{
    public GameObject shield;
    public GameObject golem;
    public Transform PosiInstantiate;
    public float cooldownTime = 1;
    public Image imageCooldown;
    public Image imageCooldown2;

    private float nextFireTime = 0;
    public Animator anim;

    public AudioClip conj1;
    public AudioClip conj2;

    AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetBool("Skill", true);
                    PhotonNetwork.Instantiate("Shield", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(conj1, transform.position);
                    anim.SetBool("Skill", false);


                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    anim.SetBool("SkillII", true);
                    PhotonNetwork.Instantiate("Golem", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown2.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(conj2, transform.position);
                    anim.SetBool("SkillII", false);


                }
            }
        }
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
