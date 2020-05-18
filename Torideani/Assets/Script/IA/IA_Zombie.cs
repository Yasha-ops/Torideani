using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Zombie : MonoBehaviour
{
    // je sais pas a quoi ca sert mais ils sont la
    public Transform groundCheck;
    public bool isGrounded;
    private Animator Anim;

    public int Hp;

    public float groundDistance = 0f;
    public LayerMask groundMask;
    private float gravity = -9.81f;

    // variables pour destination
    public GameObject joueurSolo;
    private Transform destination;
    public bool proche;
    public float speed;
    public bool death = false;
    public bool Death => death;
    private float damage;
    public float Damage
    {
        get {return damage;}
        set {damage = value;}
    }

    // variables pour l'attaque
    public float timebeforAttaque;
    public float animAttaque;
    public bool attaque;
    public GameObject setup;
    private bool unefois = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGetter();
        Hp = 100;
        Anim = GetComponent<Animator>();
        destination = joueurSolo.transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (joueurSolo == null)
            return;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (Hp > 0)
        {
            animAttaque -= Time.deltaTime;
            timebeforAttaque -= Time.deltaTime;

            if (animAttaque <= 0f)
                move(agent);
            Animation(agent);
        }
        else
        {
            Debug.Log("Mort");
            death = true;
            Hit(agent);
        }

    }

    public void PlayerGetter()
    { 
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject playerTag in Players)
        {
            joueurSolo = playerTag;
        }
        Debug.Log("Done");
    }

    private void move(NavMeshAgent agent)
    {
        agent.destination = destination.position;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (Vector3.Distance(agent.transform.position, agent.destination) < 2f)
        {
            proche = true;
            if (timebeforAttaque < 0f)
                timebeforAttaque = 2f;
        }
        else
        {
            proche = false;
            attaque = false;
            agent.speed = speed;
        }


        if (proche && timebeforAttaque < 1f)
        {
            attaqueAnim();
            if (animAttaque > 0f && attaque)
                agent.speed = 0f;
            else if (timebeforAttaque < 1f && attaque)
            {
                attaqueVie();
                attaque = false;
            }

        }

    }



    // julien
    public void attaqueAnim()
    {

        if (! (animAttaque <= 0f && attaque == false))
            return;
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                Anim.SetTrigger("attack1");
                animAttaque = 4f;
                break;
            case 1:
                Anim.SetTrigger("attack3");
                animAttaque = 4f;
                break;
            case 2:
                Anim.SetTrigger("attack2");
                animAttaque = 4f;
                break;
            default:
                break;
        }
        attaque = true;
    }

    // yassine
    public void attaqueVie()
    {
        // enleve la vie du joueur
        joueurSolo.GetComponent<Solo_Class>().TakeDamage(damage);
    }

    public void Hit(NavMeshAgent agent)
    {
        // enleve la vie du zombie
        agent.speed = 0f;
        Anim.SetBool("Death", true);
        if (unefois)
        {
            joueurSolo.GetComponent<Solo_Class>().money += 25;
            if(Random.Range (0, 100) < 50)
            {
                if (Random.Range (1,2) == 1) // Augmente les munitions
                    joueurSolo.GetComponent<Solo_Class>().Ammo += 100;
                else // Augmente les damages
                    joueurSolo.GetComponent<Solo_Class>().Damage += 1.0f;
            }
            GameObject[] Setup = GameObject.FindGameObjectsWithTag("GameSetup");
            foreach(GameObject once in Setup)
            {
                once.GetComponent<GameSetupSolo>().Spawn();
                Debug.Log("The command is sent!");
            }
            unefois = false;
        }
        death = true;
    }

    private void Animation(NavMeshAgent agent)
    {
        if (!isGrounded)
            Anim.SetBool("Ground", true);
        else
            Anim.SetBool("Ground", false);
        Anim.SetFloat("Speed", agent.speed);
    }

    public void TakeDamage(float damage)
    {
        Hp -= (int)damage;
    }
}
