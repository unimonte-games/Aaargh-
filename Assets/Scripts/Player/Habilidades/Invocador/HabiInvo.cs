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
    
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PhotonNetwork.Instantiate("Shield", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.Conjurador1);

                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    PhotonNetwork.Instantiate("Golem", PosiInstantiate.position, Quaternion.identity);
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown2.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.Conjurador2);

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
}
