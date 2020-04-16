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


    public int health;
    public int damage;
    private bool dead;
    public string current_bonus;
    public Text infoScanner;
    public Transform rayOrigin; 
    public bool Dead => dead;
    public Text health_text;
    public GameObject canvas; 

    public int ID => PV.ViewID; 
    void Start()
    {
        health_text.text = $"{health}"; 
        PV = GetComponent<PhotonView>(); 
    }

    private void Update()
    {
        if (!PV.IsMine)
            canvas.SetActive(false); 

        if (health <= 0)
        {
            health = 0; 
            dead = true;
            Destroy(this);
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
            health -= 3; 
            health_text.text = $"{health}";
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

    public void EnableBonusScanner()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 100))
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, Color.white);
            Debug.Log("Did Hit"); 
            this.GetComponent<Player_Shooting>().NotifyText($"The Player in front of u is a {hit.transform.tag}");
        }
        else 
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                    Color.yellow); 
            Debug.Log("Did not Hit");
        } 

    }

}
