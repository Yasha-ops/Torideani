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

    public GameObject[] spawnPowerUps;
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
        if ((int)timer % 100 == 0)
        {
            Debug.Log("Generating PowerUps!");
            GameObject pos = spawnPowerUps[rnd.Next(0, spawnPowerUps.Length-1)];
            RemovePowerUps();
            PhotonNetwork.Instantiate(PowerUps.name, pos.transform.position , Quaternion.identity);
            timer = timer + 1.0f  ;
        }
    }

    private void RemovePowerUps()
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        if (powerups.Length == 0)
            return;
        foreach (var power in powerups)
        {
            power.GetComponent<PowerUp_Class>().Remove();
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
