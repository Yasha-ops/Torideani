using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IA_Zombie : MonoBehaviour
{
    public int Hp;
    public int MaxHp;

    public RectTransform healthBar;

    private Animator Anim;
    private Vector3 velocity = new Vector3(0, 0.001f, 0);
    public float deathTime = 8f;
    public Transform IA_transform;
    public ParticleSystem blood;

    public AudioClip[] bruitage;
    public AudioSource audioSource;
    public GameObject[] skins;

    // variables pour destination
    public GameObject joueurSolo;
    private Transform destination;
    public bool proche;
    public float speed;
    public bool death = false;
    public bool anime;
    public bool Death => death;
    public float damage;
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
    private float wake = 3f;

    // Start is called before the first frame update
    void Start()
    {
        MaxHp = Hp;
        skins[UnityEngine.Random.Range (0, skins.Length -1)].gameObject.SetActive(true);
        PlayerGetter();
        Anim = GetComponent<Animator>();
        destination = joueurSolo.transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                Anim.SetBool("s1",true);
                animAttaque = 1.2f;
                break;
            case 1:
                Anim.SetBool("s2", true);
                animAttaque = 0.5f;
                break;
            case 2:
                Anim.SetBool("s3", true);
                animAttaque = 1.2f;
                break;
            default:
                break;
        }
        attaque = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (joueurSolo == null)
            return;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (wake > 0f)
        {
            agent.speed = 0;
            wake -= Time.deltaTime;
        }
        else if (Hp > 0)
        {
            animAttaque -= Time.deltaTime;
            timebeforAttaque -= Time.deltaTime;
           
            move(agent);
            Animation(agent);
        }
        else
        {
            deathTime -= Time.deltaTime;
            death = true;
            Hit(agent);
        }
        healthBar.sizeDelta = new Vector2(Hp * 100 / MaxHp, healthBar.sizeDelta.y);

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


        if (Vector3.Distance(agent.transform.position, agent.destination) < 2f)
        {
            proche = true;
            if (timebeforAttaque < 0f)
            {
                timebeforAttaque = 3f;
                anime = true;
            }

            agent.speed = 0f;
        }
        else
        {
            proche = false;
            attaque = false;
            agent.speed = speed;
        }


        if (proche && timebeforAttaque < 3f && anime)
        {
            attaqueAnim();
            anime = false;

        }
        else if (proche && animAttaque < 0f && attaque)
        {
              attaqueVie();
              attaque = false;
        }


    }



    // julien
    public void attaqueAnim()
    {

        if (attaque)
            return;
        int x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                Anim.SetTrigger("attack1");
                audioSource.PlayOneShot(bruitage[0]);
                animAttaque = 1.2f;
                break;
            case 1:
                Anim.SetTrigger("attack3");
                audioSource.PlayOneShot(bruitage[1]);
                animAttaque = 0.5f;
                break;
            case 2:
                Anim.SetTrigger("attack2");
                audioSource.PlayOneShot(bruitage[2]);
                animAttaque = 1.2f;
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
        if (deathTime < 0)
            gameObject.SetActive(false);
        agent.speed = 0f;
        if (deathTime < 3f)
        {
            IA_transform.position -= velocity;
            velocity += velocity * 0.014f;
        }
        
        if (unefois)
        {
            Anim.SetTrigger("Death");
            joueurSolo.GetComponent<Solo_Class>().money += 25;
            if(Random.Range (0, 100) < 50)
            {
               joueurSolo.GetComponent<Solo_Class>().Ammo += 10;
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
        Anim.SetFloat("Speed", agent.speed);
    }

    public void TakeDamage(float damage)
    {
        Anim.SetTrigger("hit");
        blood.Play();
        Hp -= (int)damage;
    }
}
