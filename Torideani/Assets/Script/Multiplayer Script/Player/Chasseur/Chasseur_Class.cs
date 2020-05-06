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
                PV.RPC("RPC_EnableBonusLocked", RpcTarget.All);
                Debug.Log("The Bonus Locked is enabled !");
                break;
            case "Speed":
                PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All);
                Debug.Log("The Bonus Speed is enabled !");
                break;
            case "Scanner":
                EnableBonusScanner();
                Debug.Log("The Bonus Scanner is enabled !");
                break;

        }
    }

    IEnumerator BonusWaiter()
    {
        yield return new WaitForSeconds(10.0f);
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
        void  RPC_EnableBonusLocked()
        {
            this.gameObject.GetComponent<Mouvement>().enabled = false;
            //StartCoroutine (BonusWaiter());
            //this.gameObject.GetComponent<Mouvement>().enabled = true;
        }

    [PunRPC]
        void RPC_EnableBonusSpeed()
        {
            this.gameObject.GetComponent<Mouvement>().speed = 200;
            //StartCoroutine (BonusWaiter());
            //this.gameObject.GetComponent<Mouvement>().speed = 100;
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
