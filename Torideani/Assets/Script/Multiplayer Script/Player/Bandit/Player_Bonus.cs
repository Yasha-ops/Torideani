using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 
public class Player_Bonus : MonoBehaviour
{
    private string current_bonus;
    public Text info;
    private PhotonView PV; 


    void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
            InputKey();
    }

    public void InputKey() // Att l'input du bandit
    {
        if (Input.GetKey(KeyCode.E))
        {
            PV= PhotonView.Get(this);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (this.gameObject.GetComponent<Bandit_Class>().current_bonus == "")
            {
                Debug.Log("You don't have bonus yet !");
                info.text = "You don't have bonus yet !";
                Invoke("Disapear", 5);
            }
            else
            {
                Debug.Log("The Key F has been pressed and u have a bonus !");
                Applying_Bonus(this.gameObject.GetComponent<Bandit_Class>().current_bonus);
                info.text = $"The {this.GetComponent<Bandit_Class>().current_bonus} bonus is applied";
                this.GetComponent<Bandit_Class>().current_bonus = ""; // Reinitialise les bonus
                Invoke("Disapear", 5);
            }
        }
    }

    void Disapear()// Reinitialise le text
    {
        info.text = "";
    }

    public void NotifyText(string message)
    {
        info.text = message;
        Invoke("Disappear", 5);
    }


    public void Applying_Bonus(string bonus)
    {
        if (bonus == "Mini") // Applique le bonus Mini a soit meme
        {
            Debug.Log("I know that ur bonus is a Mini");
            this.GetComponent<Bandit_Class>().EnableBonus(bonus);
            return;
        }
        //Applique le restes des bonus aux chasseurs
        GameObject[] chasseur = GameObject.FindGameObjectsWithTag("Chasseur");
        Debug.Log($"There is {chasseur.Length} chasseur in the current room !");
        foreach (GameObject p in chasseur)
        {
            Debug.Log("Je suis entree dans le foreach"); 
            p.gameObject.GetComponent<Chasseur_Class>().EnableBonus(bonus);
        }

    }

    IEnumerator Waiter()
    {
        Debug.Log("The bonus just started !");
        yield return new WaitForSeconds(10);
        Debug.Log("The end of the Bonus !");
    }
}
