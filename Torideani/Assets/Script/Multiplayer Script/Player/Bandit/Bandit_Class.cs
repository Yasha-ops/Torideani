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
                PV.RPC("RPC_EnableBonusLocked", RpcTarget.All);
                Debug.Log("The Bonus Locked is enabled !");
                break;
            case "Speed":
                PV.RPC("RPC_EnableBonusSpeed", RpcTarget.All);
                Debug.Log("The Bonus Speed is enabled !");
                break;
            case "Mini": 
                PV.RPC("RPC_EnableBonusMini", RpcTarget.All);
                Debug.Log("The Bonus Mini is enabled !");
                break;
        }
    }


    IEnumerator BonusWaiter()
    {
        yield return new WaitForSeconds(10.0f);
    }

    [PunRPC]
        void TakeDamage()
        {
            health -= 0.25f;
            HealthBar.GetComponent<HealthBarHUDTester>().Hurt(0.25f);
            blood.Play();
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
            this.gameObject.GetComponent<Mouvement>().speed = 150;
            //StartCoroutine (BonusWaiter());
            //this.gameObject.GetComponent<Mouvement>().speed = 100;
        }

    [PunRPC]
        void RPC_EnableBonusMini()
        {
            this.gameObject.transform.localScale = new Vector3(0.25f, 0.25f , 0.25f);
            //StartCoroutine (BonusWaiter());
            //this.gameObject.transform.localScale = new Vector3(1.0f , 1.0f , 1.0f);
        }

    [PunRPC]
        void RPC_IncreassNumber(bool act)
        {
            if (act)
            {
                GameSetup.GS.NbrBandit++;
            }
            else
            {
                GameSetup.GS.NbrBandit--;
            }
        }

    public void Hitted(int ennemi_damage)
    {
        PV.RPC("TakeDamage", RpcTarget.All);
        Debug.Log($"Player shooted health : {health}");
    }

}
