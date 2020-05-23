using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Bot : MonoBehaviour
{
    private RaycastHit Hit;
    private CharacterController controller = null;
    public bool isGrounded;
    private Animator Anim;
    public Transform groundCheck;
    public float groundDistance = 0f;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    private float speed = 2f;


    public GameObject[] skins;

    // variables pour les differentes destinations

    public Transform[] destinations;
    public bool estArrivée;
    public float waitTime;
    public float StartWatiTime;
    private float StoppingDistance = 1f;

    // variables pour l'aleatoire
    public float waitTimeAnim;
    public float waitTimeStop;

    // variables pour l'attente entre les actions
    public float coolDownAnim = 10f;
    public float coolDownStop;


    // Start is called before the first frame update
    void Start()
    {
        skins[UnityEngine.Random.Range (0, skins.Length -1)].gameObject.SetActive(true);
        waitTime = StartWatiTime;
        Anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = StoppingDistance;
        agent.speed = speed;
        agent.SetDestination(destinations[Random.Range(0, destinations.Length)].position);

    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        move(agent);
        animation(agent);
        RandomStop(agent);
        RandomAnimation(agent);
        StopWhenAnim(agent);

        coolDownStop -= Time.deltaTime;
        coolDownAnim -= Time.deltaTime;
        waitTimeAnim -= Time.deltaTime;
        waitTimeStop -= Time.deltaTime;
    }
    // il faut rajouter une fonction pour ramdomizer la Stopping distance
    private void move(NavMeshAgent agent)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (Vector3.Distance(agent.transform.position, agent.destination) < StoppingDistance)
        {
            estArrivée = true;
            StoppingDistance = Random.Range(0.4f, 10f);
            agent.stoppingDistance = StoppingDistance;

        }



        if (estArrivée)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                agent.speed = 0;
            }
            else
            {
                waitTime = Random.Range(0, 10);
                agent.speed = speed;
                agent.destination = destinations[Random.Range(0, destinations.Length)].position;
                estArrivée = false;
            }
        }
    }




    private void RandomAnimation(NavMeshAgent agent)
    {

        if (coolDownAnim < 0)
        {
            int aleatoireAnim = Random.Range(1, 5001);

            if (waitTimeAnim < 0 && aleatoireAnim == 1)
            {
                Debug.Log(aleatoireAnim);
                if (agent.speed == 0)
                {
                    animstop();
                    waitTimeAnim = Random.Range(2, 10);
                    waitTimeStop = waitTimeAnim + Random.Range(0, 4);
                    coolDownAnim = waitTimeAnim + Random.Range(25, 40);
                    coolDownStop = waitTimeStop + Random.Range(5, 20);
                }

                else
                {
                    animWalk();
                    waitTimeAnim = Random.Range(2, 10);
                    coolDownAnim = waitTimeAnim + Random.Range(25, 40);

                }
            }
        }
        else
        {
            if (waitTimeAnim < 0)
            {
                noAnim();
            }
        }
    }

    private void RandomStop(NavMeshAgent agent)
    {
        if (coolDownStop < 0)
        {
            int aleatoireStop = Random.Range(1, 501);

            if (waitTimeStop < 0 && aleatoireStop == 1)
            {
                agent.speed = 0;
                waitTimeStop = Random.Range(0, 15);
                coolDownStop = waitTimeStop + Random.Range(5, 15);
            }
        }
        else
        {
            if (waitTimeStop < 0)
            {
                agent.speed = speed;
            }
        }
    }

    public void StopWhenAnim(NavMeshAgent agent)
    {
        if (waitTimeAnim > 0f && waitTimeStop > 0f)
            agent.speed = 0f;
    }



    private void animation(NavMeshAgent agent)
    {
        if (!isGrounded)
        {
            Anim.SetBool("Ground", true);
        }
        else
        {
            Anim.SetBool("Ground", false);
        }
        Anim.SetFloat("Speed", agent.speed / 6);

    }

    private void animstop()
    {
        Anim.SetTrigger("enter");
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                Anim.SetBool("belly", true);
                break;
            case 1:
                Anim.SetBool("breakdance", true);
                break;
            case 2:
                Anim.SetBool("sittingyell", true);
                break;
            case 3:
                Anim.SetBool("lay", true);
                break;
            default:
                break;
        }
    }

    private void animWalk()
    {
        Anim.SetTrigger("enter");
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                Anim.SetBool("swing", true);
                break;
            case 1:
                Anim.SetBool("swimming", true);
                break;
            case 2:
                Anim.SetBool("jazz", true);
                break;
            case 3:
                Anim.SetBool("hiphop", true);
                break;
            default:
                break;
        }
    }

    private void noAnim()
    {
        Anim.SetBool("swing", false);
        Anim.SetBool("belly", false);
        Anim.SetBool("swimming", false);
        Anim.SetBool("breakdance", false);
        Anim.SetBool("jazz", false);
        Anim.SetBool("hiphop", false);
        Anim.SetBool("sittingyell", false);
        Anim.SetBool("lay", false);
    }
}
