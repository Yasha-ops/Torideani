using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Class : MonoBehaviour
{
    // Start is called before the first frame update

    private float currentTime = 0f;
    private float startingTime = 10f; 
    
    private int health = 5;
    private int score = 0;

    public int Health => health;
    public int Score => score;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
