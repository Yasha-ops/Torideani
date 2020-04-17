using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class Player_Shooting : MonoBehaviour
{
    private PhotonView PV;
    private Chasseur_Class avatarSetup;
    public Transform rayOrigin;
    public Text info;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<Chasseur_Class>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            InputKey();
        }
    }


    public void InputKey()
    {
        if (Input.GetKey(KeyCode.E))
        {
            PV= PhotonView.Get(this);
            RPC_Shooting();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (this.gameObject.GetComponent<Chasseur_Class>().current_bonus == "")
            {
                Debug.Log("You don't have bonus yet !");
                info.text = "You don't have bonus yet !";
                Invoke("Disapear", 5);
            }
            else
            { 
                Applying_Bonus(this.gameObject.GetComponent<Chasseur_Class>().current_bonus);
                info.text = $"The {this.GetComponent<Chasseur_Class>().current_bonus} bonus is applied";
                this.GetComponent<Chasseur_Class>().current_bonus = "";
                Invoke("Disapear", 5);
            }
        }
    }

    void Disapear()
    {
        info.text = "";
    }

    public void Applying_Bonus(string bonus)
    {
        GameObject[] bandits = GameObject.FindGameObjectsWithTag("Bandit");
        Debug.Log($"There is {bandits.Length} bandits in the current room !");
        foreach (GameObject p in bandits)
        {
            Debug.Log("Je suis entree dans le foreach"); 
            p.gameObject.GetComponent<Bandit_Class>().EnableBonus(bonus);
        }

    }




    public void RPC_Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 100))
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, Color.white);
            Debug.Log("Did Hit"); 
            if (hit.transform.tag == "PNJ")
            {
                avatarSetup.Hitted(4); 
            }
            if (hit.transform.tag == "Bandit")
            {
                hit.transform.gameObject.GetComponent<Bandit_Class>().Hitted(3);
            }
        }
        else 
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                    Color.yellow); 
            Debug.Log("Did not Hit");
        } 
    }
}
