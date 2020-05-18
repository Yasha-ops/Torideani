using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupSolo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject package;
    public GameObject[] spawnPoints;
    public GameObject player;
    public List<string> level;
    private int currentScore;
    private bool pauseVague = false;
    public float targetTime = 120.0f;
    private bool att = false;

 

    void Start()
    {
        GenerateObject(package, 3);
    }

    void Update()
    {
        currentScore = player.GetComponent<Solo_Class>().Score;
        if (currentScore == 2000)
            level.Add("level1");
        if (currentScore == 3000)
            level.Add("level2");
        if (currentScore == 6000)
            level.Add("level3");
        if (currentScore == 1000 || att)
        {
            att = true;
            targetTime -= Time.deltaTime;
            pauseVague = true;
            if (targetTime <= 0.0f || Input.GetKey(KeyCode.P))
            {
                player.GetComponent<Solo_Class>().Score += 1;
                pauseVague = false;
                att = false;
                GenerateObject(package, 20);
            }
        }

    }


    void GenerateObject(GameObject go, int amount)
    {
        if (go == null) return;
        for(int i = 0; i < amount; i++)
        {
            var randomIndex = Random.Range (0, spawnPoints.Length-1);
            Vector3 position = spawnPoints[randomIndex].gameObject.transform.position;
            GameObject tmp = Instantiate(go);
            if (level.Contains("level1"))
                tmp.GetComponent<IA_Zombie>().Hp = 30;
            if (level.Contains("level2"))
                tmp.GetComponent<IA_Zombie>().speed += 3;
            tmp.gameObject.transform.position = position;
        }
    }

    public void Spawn()
    {
        if (pauseVague)
            return;
        if (Random.Range (0,100) < 75) // 75 % de chance
        {
            GenerateObject(package, 2);
        }
    }
}
