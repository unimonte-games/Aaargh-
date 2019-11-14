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
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Time.time > nextFireTime)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    Caixa();
                    Debug.Log("Deu tiro");
                    nextFireTime = Time.time + cooldownTime;
                    imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
                    SoundManager.PlaySound(SoundManager.Sound.Boba2);

                }
            }
        }
            
    }

    void Caixa()
    {
        PhotonNetwork.Instantiate("Caixa", PosiInstantiate.position, Quaternion.identity);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
