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

    private const float Y_ANGLE_MIN = -3.0f;
    private const float Y_ANGLE_MAX = 3.0f;
    private float currentY = 0.0f;
    public GameObject gunfire;
    public GameObject gun;
    public ParticleSystem fire;
    private float animfire;
    private bool recharge;
    public float speedflip = 50f;
    private float gunrotay;


    private Animator Anim;

    public float interval = 2f;
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
        gunrotay = gun.transform.localEulerAngles.y;
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<Chasseur_Class>();
        Anim = GetComponent<Animator>();
        gun.gameObject.SetActive(false);
        currentInterval = interval;
        Ammo_Text.text = $"{chargeurCapacity - currentAmmoChargeur} / {chargeurCapacity}";
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (currentAmmoChargeur == chargeurCapacity)
            {
                Ammo_Text.color = Color.red;
                Recharge();
            }
            if (Input.GetKeyDown("r") || recharge)
            {
                recharge = true;
                Recharge();
            }

            InputKey();
            if (currentInterval < interval)
            {
                gunfire.gameObject.SetActive(false);
                gun.gameObject.SetActive(true);
            }
            else if (Input.GetButton("Fire2"))
            {
                currentY -= Input.GetAxis("Mouse Y");
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
               
                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x,
                    gunrotay - currentY, gun.transform.localEulerAngles.z);

                gunfire.gameObject.SetActive(false);
                gun.gameObject.SetActive(true);
            }
            else if (animfire < 0f)
            {
                gunfire.gameObject.SetActive(true);
                gun.gameObject.SetActive(false);
            }
            else
            {
                animfire -= Time.deltaTime;
            }
        }
    }

    public void gunflip()
    {
        gun.transform.localEulerAngles = new Vector3 (gun.transform.localEulerAngles.x, 
            gun.transform.localEulerAngles.y + speedflip , gun.transform.localEulerAngles.z);
    }

    public void Recharge()
    {
        currentInterval -= Time.deltaTime;
        this.GetComponent<Mouvement>().isShootPossible = false;
        loading.SetActive(true);
        gunflip();
        if (unefois)
        {
            Anim.SetTrigger("reload");
            audio.clip = clips[1];
            audio.Play();
            unefois = false;
        }

        if (currentInterval <= 0f)
        {
            gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x,
                gunrotay, gun.transform.localEulerAngles.z);
            loading.SetActive(false);
            Ammo_Text.color = Color.white;
            Ammo_Text.text = $"{chargeurCapacity} / {chargeurCapacity}";
            currentAmmoChargeur = 0;
            this.GetComponent<Mouvement>().isShootPossible = true;
            currentInterval = interval;
            unefois = true;
            recharge = false;
        }

    }
    public void InputKey() // Att les inputs du chasseur
    {
        if (Input.GetButtonDown("Fire1") && this.GetComponent<Mouvement>().isShootPossible)
        {
            currentAmmoChargeur++;
            Ammo_Text.text = $"{chargeurCapacity - currentAmmoChargeur} / {chargeurCapacity}";
            PV= PhotonView.Get(this);
            Anim.SetTrigger("shoot");
            gunfire.gameObject.SetActive(false);
            gun.gameObject.SetActive(true);
            animfire = 0.75f;
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
                if (hit.transform.gameObject.GetComponent<Bandit_Class>().isInTrain)
                    return;
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
