using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class Player_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    private void Start()
    {
        Random rnd = new Random();
        Vector3 test = new Vector3(rnd.Next(11), rnd.Next(11),0);
        transform.position = test; 
        PhotonNetwork.Instantiate(playerPrefab.name, test, Quaternion.identity);
        
    }
}
