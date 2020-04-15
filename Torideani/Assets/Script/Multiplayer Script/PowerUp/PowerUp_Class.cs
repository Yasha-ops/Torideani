using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;
using Photon.Realtime;

public class PowerUp_Class : MonoBehaviour
{
    // Start is called before the first frame update

    private string itsPowerUps; 
    public string[] bonusOfTheGame;
    private Random rnd = new Random();

    void Start()
    {
        itsPowerUps = bonusOfTheGame[rnd.Next(0, bonusOfTheGame.Length-1)];
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Chasseur")
        {
            this.gameObject.SetActive(false);
             Destroy(this);
        }
    }
}
