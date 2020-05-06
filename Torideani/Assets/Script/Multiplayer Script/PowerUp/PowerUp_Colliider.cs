using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Colliider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collider detected !");
        if (col.gameObject.tag == "Chasseur")
        {
            Debug.Log($"Collision with a {col.gameObject.tag} is detected !");
            col.gameObject.GetComponent<Chasseur_Class>().current_bonus = this.gameObject.GetComponent<PowerUp_Class>().itsPowerUpsChasseur;
            col.gameObject.GetComponent<Chasseur_Class>().PlaySound();
            this.GetComponent<PowerUp_Class>().Remove();
            col.gameObject.GetComponent<Player_Shooting>().NotifyText($"You got a new {this.GetComponent<PowerUp_Class>().itsPowerUpsChasseur} bonus");
        }
        if (col.gameObject.tag == "Bandit")
        {
            Debug.Log($"Collision with a {col.gameObject.tag} is detected !");
            col.gameObject.GetComponent<Bandit_Class>().current_bonus = this.gameObject.GetComponent<PowerUp_Class>().itsPowerUpsBandit;
            this.GetComponent<PowerUp_Class>().Remove();
             col.gameObject.GetComponent<Player_Bonus>().NotifyText($"You got a new {this.gameObject.GetComponent<PowerUp_Class>().itsPowerUpsBandit} bonus");

        }
    }

}
