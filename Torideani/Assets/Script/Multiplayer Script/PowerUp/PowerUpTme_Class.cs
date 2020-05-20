using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTme_Class : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collider detected !");
        if (col.gameObject.tag == "Chasseur" ||col.gameObject.tag == "Bandit" )
        {
            GameObject[] gamesetup = GameObject.FindGameObjectsWithTag("GameSetup");
            gamesetup[0].GetComponent<GameSetup>().GameDuration -= 30f;
            this.GetComponent<PowerUp_Class>().Remove();
        }
    }

}
