using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameSetupSolo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] package;
    public GameObject[] spawnPoints;
    public GameObject player;
    public List<string> level;
    private int currentScore;
    private bool pauseVague = false;
    public float targetTime = 120.0f;
    private bool att = false;
    private bool unefois = true;
    public int RoundNumber;
    public float VagueWait = 10f;
    private float currentWait = 0f;


    void Start()
    {
        RoundNumber = 1;
        currentWait = VagueWait;
        GenerateObject(package[0], 6);
    }


    private int ZombieNumber(int RoundNumber)
    {
        return Convert.ToInt32(0.000058f*Math.Pow(RoundNumber,3) + 0.074032f*Math.Pow(RoundNumber,2)+0.718119*RoundNumber+14.738698);
    }

    void Update()
    {
        int nbr = 0;
        GameObject[] Zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach(var zombie in Zombies)
        {
            if (!zombie.GetComponent<IA_Zombie>().death)
            {
                nbr++;
            }
        }
        if (nbr == 0) // Le round n'est pas encore fini
        {
            currentWait -= Time.deltaTime;
            if (currentWait <= 0)
            {
                currentWait = VagueWait;
                nbr = 1;
                unefois = false;
                GO(ZombieNumber(RoundNumber));
            }
        }
    }

    void GO(int amount)
    {
        int div = (RoundNumber / 10) + 1;
        int nbrZombie = amount / div; // Combien de type de zombie
        for(int i = 0; i != div ; i++)
        {
            if (i < package.Length)
                GenerateObject(package[i], nbrZombie);
        }
        RoundNumber++;
        player.GetComponent<Solo_Class>().Vague_Text.text = $"Wave number : {RoundNumber}";
    }

    void GenerateObject(GameObject go, int amount)
    {
        if (go == null) return;
        for(int i = 0; i < amount; i++)
        {
            var randomIndex = UnityEngine.Random.Range (0, spawnPoints.Length-1);
            Vector3 position = spawnPoints[randomIndex].gameObject.transform.position;
            GameObject tmp = Instantiate(go);
            float x = (float)(UnityEngine.Random.Range(0, 150)) / 100f;
            int y = UnityEngine.Random.Range(0, 100);
            if (RoundNumber < 6)
            {
                tmp.GetComponent<IA_Zombie>().Hp += y;
                tmp.GetComponent<IA_Zombie>().speed += x + RoundNumber / 10;

            }
            else
            { // Augmentation de 10% des que le round 10 est passé !
                tmp.GetComponent<IA_Zombie>().Hp += (int)(RoundNumber * 10) + y;
                tmp.GetComponent<IA_Zombie>().speed += (float)(RoundNumber / 10f) + x;
                tmp.GetComponent<IA_Zombie>().Damage += (int) (RoundNumber / 10);
            }
            tmp.gameObject.transform.position = position;
        }
    }

    public void Spawn()
    {
    }
}
