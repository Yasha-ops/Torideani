using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;
using Cinemachine;
public class Player_Spawner : MonoBehaviour
{
    public GameObject[] PrefabsChasseur; 
    public GameObject[] PrefabsBandits; 
    public CinemachineVirtualCamera camera;
    public CinemachineVirtualCamera cameraAim;

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
            GameObject Player = PhotonNetwork.Instantiate(
                    PrefabsChasseur[rnd.Next(0, PrefabsChasseur.Length)].name, 
                    test, Quaternion.identity);
            //camera.gameObject.transform.position = Player.GetComponent<Mouvement>().CamerePosition.transform.transform.position;
            UnityEngine.Transform pointPosition = Player.GetComponent<Mouvement>().Aim.transform;
            camera.GetComponent<CinemachineVirtualCamera>().LookAt = pointPosition;
            camera.GetComponent<CinemachineVirtualCamera>().Follow = pointPosition;
            cameraAim.GetComponent<CinemachineVirtualCamera>().LookAt = Player.GetComponent<Player_Shooting>().rayOrigin;
            cameraAim.GetComponent<CinemachineVirtualCamera>().Follow = pointPosition;
        }
        else // Bandits
        {
            GameObject Player = PhotonNetwork.Instantiate(
                    PrefabsBandits[rnd.Next(0, PrefabsBandits.Length)].name, 
                    GameSetup.GS.spawnPointsTeamTwo[rnd.Next(0, GameSetup.GS.spawnPointsTeamTwo.Length)].position, 
                    Quaternion.identity);
            UnityEngine.Transform pointPosition = Player.GetComponent<Mouvement>().Aim.transform;
            camera.GetComponent<CinemachineVirtualCamera>().LookAt = pointPosition;
            camera.GetComponent<CinemachineVirtualCamera>().Follow = pointPosition;
        }

    }
}
