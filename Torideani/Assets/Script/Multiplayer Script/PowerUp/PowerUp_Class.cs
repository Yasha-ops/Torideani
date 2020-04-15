using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PowerUp_Class : MonoBehaviour
{
    // Start is called before the first frame update

    public string itsPowerUps; 
    public string[] bonusOfTheGame;
    private Random rnd = new Random();
    public PhotonView PV;


    void Start()
    {
        itsPowerUps = bonusOfTheGame[rnd.Next(0, bonusOfTheGame.Length-1)];
    }

    public void Remove()
    {
        Debug.Log("The Remove script is passed !");
        PV.RPC("RPC_Remove", RpcTarget.All);
    }


    [PunRPC]
        void RPC_Remove()
        {
            this.gameObject.SetActive(false);
        }
}
