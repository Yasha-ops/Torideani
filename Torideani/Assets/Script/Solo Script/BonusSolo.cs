using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BonusSolo : MonoBehaviour
{
    public int Price;
    public string bonusType;

    void OnTriggerEnter(Collider col) //trouver un moyen de le faire qu'une fois
    {
        Debug.Log("Collider detected !");
        if (col.gameObject.tag == "Player" && col.GetComponent<Solo_Class>().money >= Price)
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
            col.GetComponent<Solo_Class>().Money_Text.text =$"{col.GetComponent<Solo_Class>().money}";
        }
    }

    private void Ammo(Collider col)
    {
        int ammo = col.GetComponent<Solo_Class>().Ammo;
        if (ammo > 1000)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        col.GetComponent<Solo_Class>().Ammo += 100;
        col.GetComponent<Solo_Class>().Ammo_Text.text =$"{col.GetComponent<Solo_Class>().Ammo}";
    }

    private void Recharge(Collider col)
    {
        int recharge = col.GetComponent<Solo_Class>().chargeurCapacity;
        if (recharge > 50)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        col.GetComponent<Solo_Class>().chargeurCapacity += 5;
    }

    private void Health(Collider col)
    {
        float health = col.GetComponent<Solo_Class>().Health;
        if (health > 30)
            return;
        col.GetComponent<Solo_Class>().money -= Price;
        col.GetComponent<Solo_Class>().Health += 5;
        col.GetComponent<Solo_Class>().HealthBar.GetComponent<HealthBarHUDTester>().Heal(health+2);
    }

    private void Damage(Collider col)
    {
        float damage = col.GetComponent<Solo_Class>().Damage;
        if (damage > 10f)
            return;
        col.GetComponent<Solo_Class>().Damage += (damage*2)/3;
        col.GetComponent<Solo_Class>().money -= Price;
    }


}
