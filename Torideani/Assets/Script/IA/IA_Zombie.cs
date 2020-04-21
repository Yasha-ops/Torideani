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

    private int Hp;

    public float groundDistance = 0f;
    public LayerMask groundMask;
    private float gravity = -9.81f;

    // variables pour destination
    public GameObject joueurSolo;
    private Transform destination;
    public bool proche;
    public float speed;
    private bool death;
    public bool Death => death;

    // variables pour l'attaque
    public float timebeforAttaque;
    public float animAttaque;
    public bool attaque;


    // Start is called before the first frame update
    void Start()
    {
        death = false;
        destination = joueurSolo.transform;
        Hp = 10;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
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
            death = true;
            Hit(agent);
        }

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
            {
                agent.speed = 0f;
            }
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

        if (animAttaque <= 0f && attaque == false)
        {
            int x = Random.Range(0, 3);
            if (x == 0)
            { 
                Anim.SetTrigger("attack1");
                animAttaque = 4f;
            }

            if (x == 1)
            {
                Anim.SetTrigger("attack3");
                animAttaque = 4f;
            }

            if (x == 2)
            {
                Anim.SetTrigger("attack2");
                animAttaque = 4f;
            } 
            attaque = true;
        }
    }

    // yassine
    public void attaqueVie()
    {
        // enleve la vie du joueur
        joueurSolo.GetComponent<Solo_Class>().TakeDamage();
    }

    public void Hit(NavMeshAgent agent)
    {
        // enleve la vie du zombie
        agent.speed = 0f;
        Anim.SetBool("Death", true);
    }

    private void Animation(NavMeshAgent agent)
    {
        if (!isGrounded)
        {
            Anim.SetBool("Ground", true);
        }
        else
        {
            Anim.SetBool("Ground", false);
        }
        Anim.SetFloat("Speed", agent.speed);
    }

    public void TakeDamage(float damage)
    {
        Hp -= (int)damage;
        Debug.Log($"I am a Zombie and i have {Hp} health points");
    }
}
