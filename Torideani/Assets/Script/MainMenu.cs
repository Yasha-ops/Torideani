using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
   [SerializeField] private GameObject findOpponentPanel = null;
   [SerializeField] private GameObject waitingStatusPanel = null;
   [SerializeField] private Text waitingStatusText = null;

   private bool isConnecting = false;

   private const string GameVersion = "0.1";
   private const int MaxPlayerPerRoom = 1;

   private void Awake()
   {
       PhotonNetwork.AutomaticallySyncScene = true; 
   }

   public void FindOpponent()
   {
       isConnecting = true; 
       
       findOpponentPanel.SetActive(false); 
       waitingStatusPanel.SetActive(true);

       waitingStatusText.text = "Searching ... ";

       if (PhotonNetwork.IsConnected)
       {
           PhotonNetwork.JoinRandomRoom();
       }
       else
       {
           PhotonNetwork.GameVersion = GameVersion;
           PhotonNetwork.ConnectUsingSettings();
       }
   }

   public override void OnConnectedToMaster()
   {
       Debug.Log("Connected To Master");

       //if (!PhotonNetwork.InLobby)
       //    PhotonNetwork.JoinLobby();
       if (isConnecting)
       {
           PhotonNetwork.JoinRandomRoom();
       }
   }

   public override void OnDisconnected(DisconnectCause cause)
   {
       waitingStatusPanel.SetActive(false);
       waitingStatusPanel.SetActive(true);
       Debug.Log($"Disconnected due to: {cause}");
   }

   public override void OnJoinRandomFailed(short returnCode, string message)
   {
       Debug.Log("No clients waiting for an opponent, creating a new room ...");
       
       PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayerPerRoom}); 
   }

   public override void OnJoinedRoom()
   {
       Debug.Log("Client successfully joined a room ");

       int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

       if (playerCount != MaxPlayerPerRoom)
       {
           waitingStatusText.text = "Waiting for Opponent";
           Debug.Log("Client is waiting for an opponent");
       }
       else
       {
           waitingStatusText.text = "Opponent Found";
           if (PhotonNetwork.IsMasterClient) 
               PhotonNetwork.LoadLevel(1);
       }
   }

   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
       if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
       {
           waitingStatusText.text = "Opponent Found"; 
           Debug.Log("Match is ready to begin ");
            Debug.Log(PhotonNetwork.IsMasterClient);
           if (PhotonNetwork.IsMasterClient) 
               PhotonNetwork.LoadLevel(1);
           Debug.Log("Normal");
           PhotonNetwork.CurrentRoom.IsOpen = false;
       }
   }
}
