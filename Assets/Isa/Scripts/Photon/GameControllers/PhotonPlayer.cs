using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;

    private GameSetup GS;

    // Start is called before the first frame update
    private void Awake()
    {
        GS = FindObjectOfType<GameSetup>();
    }
    void Start()
    {
        
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GS.spawnPoints.Length);
        if(PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
