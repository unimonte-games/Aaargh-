using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonManager room;
    private PhotonView PV;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
        if (PhotonManager.room == null)
        {
            PhotonManager.room = this;
        }
        else
        {
            if (PhotonManager.room != this)
            {
                Destroy(PhotonManager.room.gameObject);
                PhotonManager.room = this;
            }
        }
    }

    void Start()
    {
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon.");
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
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
        Debug.Log("We are now in a room.");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("Loading Level");
        //mudar essa primeira cena
        PhotonNetwork.LoadLevel(4);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CreatePlayer();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new Room");
        RoomOptions roomOps = new RoomOptions()
        { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room", roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed.");
        CreateRoom();
    }

    private GameSetup GS;

    private void CreatePlayer()
    {
        //PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity, 0);
        //alterar onde spawnar

        GS = FindObjectOfType<GameSetup>();
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GS.spawnPoints.Length);

        if (parr == true)
        {
            PhotonNetwork.Instantiate("Parrudinho",GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
            DontDestroyOnLoad(this.gameObject);
        }
        if (cozi == true)
        {
            PhotonNetwork.Instantiate("Cozinheiro", GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
        }
        if (boba == true)
        {
            PhotonNetwork.Instantiate("Boba da Corte", GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
        }
        if (conj == true)
        {
            PhotonNetwork.Instantiate("Conjurador", GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }

    //Players
    public GameObject Parrudinho;
    public GameObject Cozinheiro;
    public GameObject Boba;
    public GameObject Conjurador;

    private bool parr = false;
    private bool cozi = false;
    private bool boba = false;
    private bool conj = false;

    public void onParrudinhoClicked()
    {
        parr = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public void onCozinheiroClicked()
    {
        cozi = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public void onBobaClicked()
    {
        boba = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public void onConjuradorClicked()
    {
        conj = true;
        PhotonNetwork.JoinRandomRoom();
    }

}
