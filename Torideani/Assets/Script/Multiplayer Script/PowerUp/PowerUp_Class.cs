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

    public string itsPowerUpsChasseur; 
    public string itsPowerUpsBandit;
    public string[] bonusOfTheGameChasseur;
    public string[] bonusOfTheGameBandit;
    private Random rnd = new Random();
    public PhotonView PV;


    void Start()
    {
        itsPowerUpsBandit = bonusOfTheGameBandit[rnd.Next(0, bonusOfTheGameBandit.Length-1)];
        itsPowerUpsChasseur = bonusOfTheGameChasseur[rnd.Next(0, bonusOfTheGameChasseur.Length-1)];
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
            Destroy(this);
        }
}
