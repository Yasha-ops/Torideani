using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Class : MonoBehaviour
{
    private float health;
    private float damage;

    public float Health => health;

    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("You just killed a Zombie !");
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }


     private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collider detected !");
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Solo_Class>().TakeDamage();
        }
    }

}
