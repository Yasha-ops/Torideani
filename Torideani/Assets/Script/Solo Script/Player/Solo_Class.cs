using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solo_Class : MonoBehaviour
{
    private float health;
    private int ammo;
    public int Ammo
    {
        get {return ammo ;}
        set {ammo = value;}
    }
    private float damage;
    public float Damage
    {
        get{return damage;}
        set{damage = value;}
    }
    private int score;
    public int Score
    {
        get{return score;} 
        set{score = value;}
    }

    public int money;
    public Transform rayOrigin;
    public ParticleSystem feu; 
    public Text Score_Text;
    public Text Ammo_Text;
    public Text Money_Text;
    public ParticleSystem blood;
    private float interval = 5.0f;
    private float currentInterval;
    public int chargeurCapacity = 10;
    private int currentAmmoChargeur = 0;
    public bool isShootPossible = true;

    public GameObject HealthBar;
    void Start()
    {
        currentInterval = interval;
        HealthBar.GetComponent<HealthBarHUDTester>().Hurt(0.25f);
        health = 20;
        ammo = 400;
        damage = 2.0f;
    }

    void Update()
    {
        if (currentAmmoChargeur == chargeurCapacity)
        {
            currentInterval -= Time.deltaTime;
            isShootPossible = false;
            if (currentInterval <= 0f)
            {
                currentAmmoChargeur = 0;
                isShootPossible = true;
                currentInterval = interval;
            }
        }
    }

    public void TakeInput()
    {
        if (!isShootPossible)
            return;
        if (ammo > 0)
        {
            Shoots();
            Ammo_Text.text = $"{ammo}";
            Money_Text.text = $"{money}";
            ammo--;
            currentAmmoChargeur++;
            Debug.Log(currentAmmoChargeur);
        }
    }

    public void Shoots()
    {
        feu.Play();
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 100))
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, Color.white);
            Debug.Log("Did Hit"); 
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Zombie" && !hit.transform.gameObject.GetComponent<IA_Zombie>().Death)
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                        Color.red);
                score += 100;
                Score_Text.text = $"Score : {score}";
                hit.transform.gameObject.GetComponent<IA_Zombie>().TakeDamage(damage);
            }
        }
        else 
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                    Color.yellow); 
        } 
    }



    public void TakeDamage()
    {
        if (health <= 0)
        {
            return;
        }
        health -= 0.25f;
        blood.Play();
        HealthBar.GetComponent<HealthBarHUDTester>().Hurt(0.25f);
    }
}
