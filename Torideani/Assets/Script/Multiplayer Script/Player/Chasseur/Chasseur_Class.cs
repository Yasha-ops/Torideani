using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI; 

public class Chasseur_Class : MonoBehaviour
{
    // Start is called before the first frame update

    private PhotonView PV;


    public int health;
    public int damage;
    private bool dead;
    public string current_bonus;



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
}
