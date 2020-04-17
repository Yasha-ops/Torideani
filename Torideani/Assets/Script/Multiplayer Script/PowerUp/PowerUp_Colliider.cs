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
            col.gameObject.GetComponent<Chasseur_Class>().current_bonus = this.gameObject.GetComponent<PowerUp_Class>().itsPowerUps;
            this.GetComponent<PowerUp_Class>().Remove();
        }
    }

}
