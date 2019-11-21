using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    private PhotonView PV;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connected to Photon.");

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby successfully.");
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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(4);
            //mudar essa primeira cena
        }
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
        PhotonNetwork.CreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
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
