﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;


    public GameObject playButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        playButton.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play Button was click");
        playButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed. There must be no open games available");
        CreateRoom();
    }
        void CreateRoom()
    {
        Debug.Log("Trying to create a new Room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We are now in a room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        CreateRoom();
    }
    
    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        playButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
