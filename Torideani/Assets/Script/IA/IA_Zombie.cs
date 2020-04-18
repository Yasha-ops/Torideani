using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Zombie : MonoBehaviour
{
    // je sais pas a quoi ca sert mais ils sont la
    public Transform groundCheck;
    public bool isGrounded;

    public float groundDistance = 0f;
    public LayerMask groundMask;
    private float gravity = -9.81f;

    // variables pour destination
    public Transform destination;
    public bool proche;
    public float speed;

    // variables pour l'attaque
    public float animAttaque;
    public bool attaque;


    // Start is called before the first frame update
    void Start()
    {

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);

    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        move(agent);
        StopAnim(agent);

    }
    private void move(NavMeshAgent agent)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (Vector3.Distance(agent.transform.position, agent.destination) < 0.2f)
        {
            proche = true;
        }
        else
        {
            proche = false;
        }


        if (proche)
        {
            // attaqueAnim();
            if (animAttaque > 0f && attaque)
            {
                animAttaque -= Time.deltaTime;
                agent.speed = 0f;
            }
            else if (animAttaque < 0f && attaque)
            {
                attaqueVie();
                attaque = false;
            }

        }

    }

    public void StopAnim(NavMeshAgent agent)
    {
        if (proche == false)
        {
            // Stop l'animation
            agent.speed = speed;
            agent.destination = destination.position;
        }
    }

    // julien
    public void attaqueAnim()
    {

        if (animAttaque <= 0f && attaque == false)
        {
            // lance l'anim d'attaque
            // animAttaque = durée de l'anim
            attaque = true;
        }
    }

    // yassine
    public void attaqueVie()
    {

        // enleve la vie du joueur

    }
}