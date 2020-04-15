using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI; 

public class Bandit_Class : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV; 
    public GameObject me; 

    public int health;
    public int damage;
    private bool dead;
    public string current_bonus; 


    public Text healthtext;
    public GameObject canvas; 


    public bool Dead => dead;


    void Start()
    {
        healthtext.text = $"{health}";
        PV = GetComponent<PhotonView>(); 
    }

    private void Update()
    {
        healthtext.text = $"{health}";

        if (!PV.IsMine)
        {
            canvas.SetActive(false);
        }
        if (health <= 0)
        {
            health = 0; 
            dead = true;
            me.SetActive(false);
        }
    }

    public void EnableBonus(string bonus)
    {
        switch (bonus)
        {
            case "Locked":
                PV.RPC("RPC_EnableBonusLocked", RpcTarget.All);
                Debug.Log("The Bonus Locked is enabled !");
                break;
            case "Speed":
                PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All);
                Debug.Log("The Bonus Speed is enabled !");
                break; 
        }
    }


    [PunRPC]
        void TakeDamage()
        {
            health -= 3;
            healthtext.text = $"{health}";
        }

    [PunRPC]
        void  RPC_EnableBonusLocked()
        {
            this.gameObject.GetComponent<Mouvement>().enabled = false;
        }

    [PunRPC]
        void RPC_EnableBonusSpeed()
        {
            this.gameObject.GetComponent<Mouvement>().speed = 200;
        }

    public void Hitted(int ennemi_damage)
    {
        PV.RPC("TakeDamage", RpcTarget.All);
        Debug.Log($"Player shooted health : {health}");
    }

}
