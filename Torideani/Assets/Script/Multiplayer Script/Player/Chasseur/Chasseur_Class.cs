using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 

public class Chasseur_Class : MonoBehaviour
{
    // Start is called before the first frame update

    private PhotonView PV;


    public float  health;
    public int damage;
    private bool dead;
    public string current_bonus;
    public Text infoScanner;
    public Transform rayOrigin; 
    public bool Dead => dead;
    public GameObject canvas;
    public GameObject HealthBar;

    public AudioSource source;
    public Text GameOver;
    public Text Info;

    public int ID => PV.ViewID; 
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PV.RPC("RPC_IncreassNumber", RpcTarget.All, true);
    }

    private void Update()
    {
        if (!PV.IsMine)
            canvas.SetActive(false);
        if (health <= 0)
        {
            PV.RPC("RPC_IncreassNumber", RpcTarget.All, false);
            dead = true;
            this.gameObject.SetActive(false);
        }
    }

    public void PlaySound()
    {
        source.Play(0);
    }

    public void EnableBonus(string bonus)
    {
        switch (bonus)
        {
            case "Locked":
                PV.RPC("RPC_EnableBonusLocked", RpcTarget.All , true);
                Invoke("Disable1" , 10);
                break;
            case "Speed":
                PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All, true);
                Invoke("Disable2" , 10);
                break;
            case "Scanner":
                EnableBonusScanner();
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

    public void Hitted(int ennemi_damage)
    {
        PV.RPC("TakeDamage", RpcTarget.All); 
        Debug.Log($"Player shooted health : {health}");
    }


    [PunRPC]
        void TakeDamage()
        {
            health -= 0.25f; 
            HealthBar.GetComponent<HealthBarHUDTester>().Hurt(0.25f);
        }

    [PunRPC]
        void  RPC_EnableBonusLocked(bool test)
        {
            this.gameObject.GetComponent<Mouvement>().enabled = test;
            //StartCoroutine (BonusWaiter());
            //this.gameObject.GetComponent<Mouvement>().enabled = true;
        }

    [PunRPC]
        void RPC_EnableBonusSpeed(bool test)
        {
            if (test)
                this.gameObject.GetComponent<Mouvement>().speed = 12f;
            else
                this.gameObject.GetComponent<Mouvement>().speed = 6f;
        }

    [PunRPC]
        void RPC_IncreassNumber(bool act)
        {
            if (act)
            {
                GameSetup.GS.NbrChasseurs++;
            }
            else
            {
                GameSetup.GS.NbrChasseurs--;
            }
        }

    public void EnableBonusScanner()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 100))
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, Color.white);
            Debug.Log("Did Hit");
            if (hit.transform.tag == "Untagged")
                EnableBonusScanner();
            this.GetComponent<Player_Shooting>().NotifyText($"The Player in front of u is a {hit.transform.tag}");
        }
        else 
        { 
            this.GetComponent<Player_Shooting>().NotifyText($"You missed it !");
            EnableBonusScanner();
        } 

    }

}
