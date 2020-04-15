using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class Player_Spawner : MonoBehaviour
{
    public GameObject[] PrefabsChasseur; 
    public GameObject[] PrefabsBandits; 

    private void Start()
    {
        Spawn();
    }


    private void Spawn()
    {
        Random rnd = new Random();
        Vector3 test = new Vector3(rnd.Next(11), rnd.Next(11), 0);
        transform.position = test;

        if (PhotonNetwork.IsMasterClient) // Chasseur
        {
            PhotonNetwork.Instantiate(
                    PrefabsChasseur[rnd.Next(0, PrefabsChasseur.Length)].name, 
                    test, Quaternion.identity);
        }
        else // Bandits
        {
            PhotonNetwork.Instantiate(
                    PrefabsBandits[rnd.Next(0, PrefabsBandits.Length)].name, 
                    GameSetup.GS.spawnPointsTeamTwo[rnd.Next(0, GameSetup.GS.spawnPointsTeamTwo.Length)].position, 
                    Quaternion.identity);

        }
        Debug.Log("Spawning done !");


    }
}
