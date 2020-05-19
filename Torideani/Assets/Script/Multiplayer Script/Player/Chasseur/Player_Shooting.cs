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

    public GameObject gunfire;
    public GameObject gun;
    public ParticleSystem fire;

    private Animator Anim;

    public float interval = 5f;
    public float currentInterval;
    private int currentAmmoChargeur = 0;
    private int chargeurCapacity = 10;
    private bool unefois = true;
    public Text Ammo_Text;
    public GameObject loading;

    public AudioClip[] clips;
    public AudioSource audio;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<Chasseur_Class>();
        Anim = GetComponent<Animator>();
        gun.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (currentAmmoChargeur == chargeurCapacity)
            {
                currentInterval -= Time.deltaTime;
                this.GetComponent<Mouvement>().isShootPossible = false;
                Ammo_Text.color = Color.red;
                loading.SetActive(true);
                if (unefois)
                {
                    audio.clip = clips[1];
                    audio.Play();
                    unefois = false;
                }

                if (currentInterval <= 0f)
                {
                    loading.SetActive(false);
                    Ammo_Text.color = Color.white;
                    Ammo_Text.text = $"{chargeurCapacity} / {chargeurCapacity}";
                    currentAmmoChargeur = 0;
                    this.GetComponent<Mouvement>().isShootPossible = true;
                    currentInterval = interval;
                    unefois = true;
                }
            }
            InputKey();
        }
    }


    public void InputKey() // Att les inputs du chasseur
    {
        if (Input.GetKeyDown(KeyCode.E) && this.GetComponent<Mouvement>().isShootPossible)
        {
            currentAmmoChargeur++;
            Ammo_Text.text = $"{chargeurCapacity - currentAmmoChargeur} / {chargeurCapacity}";
            PV= PhotonView.Get(this);
            Anim.SetTrigger("shoot");
            gunfire.gameObject.SetActive(false);
            gun.gameObject.SetActive(true);
            fire.Play();
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
                info.text = $"The {this.GetComponent<Chasseur_Class>().current_bonus} bonus is applied";
                Applying_Bonus(this.gameObject.GetComponent<Chasseur_Class>().current_bonus);
                this.GetComponent<Chasseur_Class>().current_bonus = "";
                Invoke("Disapear", 5);
            }
        }
    }

    public void NotifyText(string message)
    {
        info.text = "";
        info.text = message;
        Invoke("Disappear", 2);
    }


    void Disapear() // Reinitialise le text
    {
        info.text = "";
    }

    public void Applying_Bonus(string bonus) // Applique bonus a tous les bandits
    {
        if (bonus == "Scanner")
        {
            this.GetComponent<Chasseur_Class>().EnableBonus("Scanner");
            return;
        }
        GameObject[] bandits = GameObject.FindGameObjectsWithTag("Bandit");
        Debug.Log($"There is {bandits.Length} bandits in the current room !");
        foreach (GameObject p in bandits)
        {
            p.gameObject.GetComponent<Bandit_Class>().EnableBonus(bonus);
        }

    }


    public void RPC_Shooting() // Fonction tirer
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
