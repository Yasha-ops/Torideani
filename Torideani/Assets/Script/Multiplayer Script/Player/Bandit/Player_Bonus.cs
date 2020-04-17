using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bonus : MonoBehaviour
{

    private string bonus;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void WichBonus(string bonus)
    {
        switch(bonus) // Besoin du resau ?? 
        {
            case "Locked":
                this.gameObject.GetComponent<Mouvement>().enabled = false;
                Waiter(); 
                this.gameObject.GetComponent<Mouvement>().enabled = true;
                this.gameObject.GetComponent<Bandit_Class>().current_bonus = ""; 

                break;
        }
    }


    IEnumerator Waiter()
    {
        Debug.Log("The bonus just started !");
        yield return new WaitForSeconds(10);
        Debug.Log("The end of the Bonus !");
    }
}
