using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BonusSolo : MonoBehaviour
{
    public int Price;
    void OnTriggerEnter(Collider col) //trouver un moyen de le faire qu'une fois
    {
        Debug.Log("Collider detected !");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log($"Collision with a {col.gameObject.tag} is detected !");
            if (col.GetComponent<Solo_Class>().money >= Price)
            {
                col.GetComponent<Solo_Class>().Damage += col.GetComponent<Solo_Class>().Damage*2;
                col.GetComponent<Solo_Class>().money -= Price;
                col.GetComponent<Solo_Class>().Money_Text.text =$"col.GetComponent<Solo_Class>().money";
                Debug.Log("Ur weapon got upgraded !");
            }
        }
    }
}
