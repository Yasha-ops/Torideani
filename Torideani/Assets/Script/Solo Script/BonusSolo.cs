using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BonusSolo : MonoBehaviour
{
    public int Price;
    public string bonusType; 
    private float timenomoney;
    public AudioSource audioSource;
    public AudioClip audioclip;

    void Start()
    {
         test.text = $"{Price} $";
    }

    private void Update()
    {
        if (timenomoney < 0f)
        {
            nomoney.text = "";
        }
        else
        {
            timenomoney -= Time.deltaTime;
        }
    }
    public TextMeshPro test;
    public UnityEngine.UI.Text nomoney;
   
    void OnTriggerEnter(Collider col) //trouver un moyen de le faire qu'une fois
    {
        Debug.Log("Collider detected !");
        if (col.GetComponent<Solo_Class>().money < Price)
        {
            nomoney.text = "not enough money";
            timenomoney = 2f;
        }
        else if (col.gameObject.tag == "Player")
        {
            switch (bonusType)
            {
                case "ammo":
                    Ammo(col);
                    break;
                case "damage":
                    Damage(col);
                    break;
                case "recharge":
                    Recharge(col);
                    break;
                case "health":
                    Health(col);
                    break;
                default:
                    break;
            }
            test.text = $"{Price} $";
            col.GetComponent<Solo_Class>().Money_Text.text =$"{col.GetComponent<Solo_Class>().money}";
        }
    }

    private void Ammo(Collider col)
    {
        int ammo = col.GetComponent<Solo_Class>().Ammo;
        if (ammo > 1000)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        audioSource.PlayOneShot(audioclip);
        Price += Price / 5;
        col.GetComponent<Solo_Class>().Ammo += 100;
        col.GetComponent<Solo_Class>().Ammo_Text.text =$"{col.GetComponent<Solo_Class>().Ammo}";
    }

    private void Recharge(Collider col)
    {
        int recharge = col.GetComponent<Solo_Class>().chargeurCapacity;
        if (recharge > 50)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        audioSource.PlayOneShot(audioclip);
        Price += Price / 2;
        col.GetComponent<Solo_Class>().chargeurCapacity += 2;
    }

    private void Health(Collider col)
    {
        float health = col.GetComponent<Solo_Class>().Health;
        if (health > 20)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        audioSource.PlayOneShot(audioclip);
        Price += Price / 5;
        col.GetComponent<Solo_Class>().Health += 5;
        col.GetComponent<Solo_Class>().HealthBar.GetComponent<HealthBarHUDTester>().Heal(health+2);
    }

    private void Damage(Collider col)
    {
        float damage = col.GetComponent<Solo_Class>().Damage;
        Debug.Log(damage);
        if (damage > 1000f)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        audioSource.PlayOneShot(audioclip);
        Price += Price / 5;
        col.GetComponent<Solo_Class>().Damage += (int) (damage / 3);
        Debug.Log(col.GetComponent<Solo_Class>().Damage);
        col.GetComponent<Solo_Class>().Money_Text.text =$"{col.GetComponent<Solo_Class>().money}";
    }


}
