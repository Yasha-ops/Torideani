using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;

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
    public GameObject PowerUpsTime;

    public GameObject firstcam;

    public GameObject music1;
    public GameObject music2;

    public float GameDuration = 300f;

    public GameObject CameraAim;

    public void CountDownTimer()
    {
        GameDuration -= Time.deltaTime;
        TextTimer.text = "Time Left:" + Mathf.Round(GameDuration);
        if(GameDuration <= 0)
            return;
    }

    public bool Finished()
    {
        GameObject[] banditList = GameObject.FindGameObjectsWithTag("Bandit");
        GameObject[] chasseursList = GameObject.FindGameObjectsWithTag("Chasseur");

        if (chasseursList.Length == 0)
        {
            foreach(GameObject bandit in banditList)
                bandit.GetComponent<Bandit_Class>().GameOver.text = "You won !";
            TextTimer.text = "GAME OVER!";
            return true;
        }
        if (banditList.Length == 0)
        {
            foreach(GameObject chasseurs in chasseursList)
                chasseurs.GetComponent<Chasseur_Class>().GameOver.text = "You won !";
            TextTimer.text = "GAME OVER!";
            return true;
        }

        if (GameDuration <=  100)
        {
            foreach(GameObject bandit in banditList)
                bandit.GetComponent<Bandit_Class>().Info.text = "The train arrived, you have to escape !";
            chasseursList[0].GetComponent<Chasseur_Class>().Info.text = "Stop the bandits or they will escape !";
        }

        if (GameDuration <= 0)
        {
            int nbr = 0;
            foreach(GameObject bandit in banditList)
            {
                if (!bandit.GetComponent<Bandit_Class>().isInTrain)
                    {
                        bandit.GetComponent<Bandit_Class>().GameOver.color = Color.red;
                        bandit.GetComponent<Bandit_Class>().GameOver.text = "You losed !";
                    }
                else
                    nbr++;
            }
            if (nbr != 0)
            {chasseursList[0].GetComponent<Chasseur_Class>().GameOver.color= Color.red;
            chasseursList[0].GetComponent<Chasseur_Class>().GameOver.text = "You losed!";}
            else
            {
                chasseursList[0].GetComponent<Chasseur_Class>().GameOver.color = Color.green;
                chasseursList[0].GetComponent<Chasseur_Class>().GameOver.text = "You win !";
            }
            return true;
        }
        return false;
    }

    void Update()
    {
        if (Finished())
        {
            Invoke("DisconnectPlayer", 10);
        }

        CountDownTimer();
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);

        if ((int)timer % 100 == 0 && GameDuration > 120)
        {
            RemovePowerUps();
            GameObject pos = spawnPowerUps[rnd.Next(0, spawnPowerUps.Length-1)];
            PhotonNetwork.Instantiate(PowerUps.name, pos.transform.position , Quaternion.identity);
            pos = spawnPowerUps[rnd.Next(0, spawnPowerUps.Length-1)];
            PhotonNetwork.Instantiate(PowerUpsTime.name, pos.transform.position , Quaternion.identity);
            timer = timer + 1.0f  ;
        }
        else if (GameDuration < 120)
        {
            music1.SetActive(false);
            music2.SetActive(true);
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


    public void DisconnectPlayer()
    {
        PhotonNetwork.Disconnect();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
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
