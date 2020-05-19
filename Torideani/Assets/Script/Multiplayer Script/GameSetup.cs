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

    public TextMesh TextTimer;

    public float timer = 0.0f;

    public GameObject[] spawnPowerUps;
    private Random rnd = new Random();

    public int NbrChasseurs = 1;
    public int NbrBandit = 1;

    private int seconds;

    public GameObject PowerUps;

    public float GameDuration = 300f;

    public GameObject CameraAim;

    public void CountDownTimer()
    {
        GameDuration -= Time.deltaTime;
        TextTimer.text = "Time Left:" + Mathf.Round(GameDuration);
        if(GameDuration< 0)
        {
            Finished(true);
        }
    }

    public bool Finished(bool shortcut)
    {
        GameObject[] banditList;
        GameObject[] chasseursList;
        banditList = GameObject.FindGameObjectsWithTag("Bandit");
        chasseursList = GameObject.FindGameObjectsWithTag("Chasseur");

        Debug.Log("NbrChasseurs " + NbrChasseurs + "NbrBandit " + NbrBandit);
        if (NbrChasseurs == 0 || shortcut)
        {
            foreach(GameObject bandit in banditList)
                bandit.GetComponent<Bandit_Class>().GameOver.text = "You won !";
            TextTimer.text = "GAME OVER!";
        }
        if (NbrBandit == 0)
        {
            foreach(GameObject chasseurs in chasseursList)
                chasseurs.GetComponent<Chasseur_Class>().GameOver.text = "You won !";
            TextTimer.text = "GAME OVER!";
        }
        return (NbrChasseurs == 0) || (NbrBandit == 0);
    }

    void Update()
    {
        //if (Finished(false))
        //{
        //    Debug.Log("The game is over !");
        //    return;
        //}
        CountDownTimer();
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
