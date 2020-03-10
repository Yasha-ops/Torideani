using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class Player_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;


    public GameObject[] PrefabsToInstantiate;

    private void Start()
    {
        Random rnd = new Random();
        Vector3 test = new Vector3(rnd.Next(11), rnd.Next(11), 0);
        transform.position = test;

        Random rnd1 = new Random();
        PhotonNetwork.Instantiate(PrefabsToInstantiate[rnd1.Next(PrefabsToInstantiate.Length)].name, test, Quaternion.identity);
        
    }
}
