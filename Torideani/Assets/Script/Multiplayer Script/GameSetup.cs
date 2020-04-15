using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    private PhotonView PV; 
    public int nextPlayersTeam; 
    public Transform[] spawnPointsTeamOne; 
    public Transform[] spawnPointsTeamTwo;

    public float timer = 0.0f;

    public Transform[] spawnPowerUps;
    private Random rnd = new Random();
    private int seconds;

    public GameObject PowerUps;


    public bool Finished()
    {
        bool exist_ch = false;
        bool exist_ba = false;
        GameObject[] bandits = GameObject.FindGameObjectsWithTag("Bandit");
        GameObject[] chasseurs = GameObject.FindGameObjectsWithTag("Chasseur");
        foreach (var ban in bandits)
        {
            if (ban.active)
                exist_ba = true;
        }
        foreach (var cha in chasseurs)
        {
            if (cha.active)
                exist_ch = true;
        }
        return (!exist_ch || !exist_ba); 
    }

    void Update()
    {

        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        if (seconds % 50 == 0)
        {
            Debug.Log("Generating PowerUps!");
            Transform pos = spawnPowerUps[rnd.Next(0, spawnPowerUps.Length-1)];
            RemovePowerUps();
            PhotonNetwork.Instantiate(PowerUps.name, pos.position, Quaternion.identity);
        }
    }

    private void RemovePowerUps()
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp"); 
        foreach (var power in powerups)
        {
            PhotonNetwork.Destroy(power);
        }
    }



    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }



    [PunRPC]
        void RPC_Finished()
        {
            Debug.Log("The Game is over !");
        }

}
