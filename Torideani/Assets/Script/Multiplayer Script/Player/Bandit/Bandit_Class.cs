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

    public float health;
    public int damage;
    private bool dead;
    public string current_bonus; 
    public ParticleSystem blood;

    public GameObject canvas; 
    public GameObject HealthBar;

    public Text GameOver;
    public Text Info;

    private int nbrCollision = 0;

    public bool isInTrain = false;
    public bool Dead => dead;


    void Start()
    {
        PV = GetComponent<PhotonView>(); 
        PV.RPC("RPC_IncreassNumber", RpcTarget.All, true);
    }

    private void Update()
    {

        if (!PV.IsMine)
        {
            canvas.SetActive(false);
        }
        if (health <= 0)
        {
            health = 0; 
            dead = true;
            PV.RPC("RPC_IncreassNumber", RpcTarget.All, false);
            me.SetActive(false);
        }
    }

    public void EnableBonus(string bonus)
    {
        switch (bonus)
        {
            case "Locked":
                PV.RPC("RPC_EnableBonusLocked", RpcTarget.All, true);
                Invoke("Disable1",10);
                break;
            case "Speed":
                PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All, true);
                Invoke("Disable2",  10);
                break;
            case "Mini": 
                PV.RPC("RPC_EnableBonusMini", RpcTarget.All, true);
                Invoke("Disable3",  10);
                break;
        }
    }

    private void Disable1()
    {
        PV.RPC("RPC_EnableBonusLocked", RpcTarget.All , false);

    }

    private void Disable2()
    {
        PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All , false);

    }

    private void Disable3()
    {
        PV.RPC("RPC_EnableBonusMini", RpcTarget.All , false);

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Chasseur")
        {
            nbrCollision++;
            if (nbrCollision > 3)
            {
                nbrCollision = 0;
                current_bonus = "Mini";
            }
        }
        if (col.gameObject.tag == "Arrived")
        {
            isInTrain = true;
            this.GetComponent<Mouvement>().enabled = false;
            GameOver.text = "YOU WON!";
        }
    }


    [PunRPC]
        void TakeDamage()
        {
            health -= 0.25f;
            HealthBar.GetComponent<HealthBarHUDTester>().Hurt(0.25f);
            blood.Play();
        }

    [PunRPC]
        void  RPC_EnableBonusLocked(bool test)
        {
            this.gameObject.GetComponent<Mouvement>().enabled = test;
        }

    [PunRPC]
        void RPC_EnableBonusSpeed(bool test)
        {
            if (test)
                this.gameObject.GetComponent<Mouvement>().speed = 10f;
            else
                this.gameObject.GetComponent<Mouvement>().speed = 6f;
        }

    [PunRPC]
        void RPC_EnableBonusMini(bool test)
        {
            if (test)
                this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f , 0.25f);
            else
                this.gameObject.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
        }

    [PunRPC]
        void RPC_IncreassNumber(bool act)
        {
            if (act)
                GameSetup.GS.NbrBandit++;
            else
                GameSetup.GS.NbrBandit--;
        }

    public void Hitted(int ennemi_damage)
    {
        PV.RPC("TakeDamage", RpcTarget.All);
        Debug.Log($"Player shooted health : {health}");
    }

}
