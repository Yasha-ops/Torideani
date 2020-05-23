using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solo_Class : MonoBehaviour
{
    private float health;
    private Animator Anim;
    public float Health
    {
        get {return health;}
        set {health = value;
        if (health > 20)
            health = 20;}
    }
    private int ammo;
    public int Ammo
    {
        get {return ammo ;}
        set {ammo = value;
        if (ammo > 1000)
            ammo = 1000;}
    }
    private int damage;
    public int Damage
    {
        get{return damage;}
        set{
            damage = value;
            if (damage > 1000)
                damage =1000;
            }
    }

    private int score;
    public int Score
    {
        get{return score;} 
        set{score = value;}
    }

    public float money;
    public Transform rayOrigin;
    public ParticleSystem feu; 
    
    public Text Score_Text;
    public Text Ammo_Text;
    public Text Money_Text;
    public Text CurrentAmmo_Text;
    public Text Vague_Text;
    public Text Damage_Text;

    public ParticleSystem blood;
    private float interval = 3.0f;
    private bool recharge;
    public float Interval
    {
        get {return interval;}
        set {interval = value;}
    }
    private float currentInterval;
    public int chargeurCapacity = 10;
    public int ChargeurCapacity
    {
        get {return chargeurCapacity;}
        set { chargeurCapacity = value;
            if (chargeurCapacity > 50)
                chargeurCapacity = 50;
        }
    }
    private int currentAmmoChargeur = 0;
    public bool isShootPossible = true;
    private bool unefois = true;
    public GameObject loading;

    public GameObject HealthBar;
    void Start()
    {
        Anim = GetComponent<Animator>();
        currentInterval = interval;
        health = 20;
        ammo = 400;
        damage = 25;
    }

    void Update()
    {
        if (health > 0)
        {
            if (currentAmmoChargeur == chargeurCapacity)
            {
                CurrentAmmo_Text.color = Color.red;
                Recharge();
            }
            if (Input.GetKeyDown("r") || recharge)
            {
                recharge = true;
                Recharge();
            }
            Money_Text.text = $"{money}";
            Damage_Text.text = $"{damage}";
            CurrentAmmo_Text.text = $"{chargeurCapacity - currentAmmoChargeur} / {chargeurCapacity}";
            Ammo_Text.text = $"{ammo}";
        }

    }

    public void Recharge()
    {
        currentInterval -= Time.deltaTime;
        isShootPossible = false;
        loading.SetActive(true);
        if (unefois)
        {
            Anim.SetTrigger("reload");
            this.GetComponent<Mouvement_Solo>().audio.clip = this.GetComponent<Mouvement_Solo>().clips[1];
            this.GetComponent<Mouvement_Solo>().audio.Play();
            unefois = false;
        }

        if (currentInterval <= 0f)
        {
            loading.SetActive(false);
            CurrentAmmo_Text.color = Color.white;
            CurrentAmmo_Text.text = $"{chargeurCapacity} / {chargeurCapacity}";
            currentAmmoChargeur = 0;
            isShootPossible = true;
            currentInterval = interval;
            unefois = true;
            recharge = false;
        }

    }
    
    public void TakeInput()
    {
        if (!isShootPossible)
            return;
        if (ammo > 0)
        {
            Shoots();
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
                score += 10;
                Score_Text.text = $"{score}";
                hit.transform.gameObject.GetComponent<IA_Zombie>().TakeDamage(damage);
            }
        }
        else 
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                    Color.yellow); 
        } 
    }



    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damage;
        Anim.SetTrigger("hit");
        blood.Play();
        HealthBar.GetComponent<HealthBarHUDTester>().Hurt(damage);
    }
}
