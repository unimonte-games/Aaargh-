using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom room;
    private PhotonView PV;

    //public bool isGameLoaded;
    public int currentScene;
    public int multiplayerScene;

    //Player info
    //Player[] photonPlayers;
    //public int playersInRoom;
    //public int myNumberInRoom;

    //public int playersInGame;

    private void Awake()
    {
         if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if(PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        //PhotonNetwork.CurrentRoom.IsOpen = false;


    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();
        //photonPlayers = PhotonNetwork.PlayerList;
        //playersInRoom = photonPlayers.Length;
        //myNumberInRoom = playersInRoom;
        //PhotonNetwork.NickName = myNumberInRoom.ToString();

    }

    void StartGame()
    {
        Debug.Log("Loading Level");
        if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == multiplayerScene)
        {
            CreatePlayer();
        }
    }
    private void CreatePlayer()
    {
        //creates player network controller but not player character
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

}
