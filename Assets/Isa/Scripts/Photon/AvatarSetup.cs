using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;
    public PlayerInfo PI;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.mySelectedCharacter);
        }
    }
    
    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        PI = FindObjectOfType<PlayerInfo>();
        characterValue = whichCharacter;
        
        myCharacter = Instantiate(Testplayer.NewPlayer(whichCharacter), transform.position, transform.rotation, transform);
    }
}
