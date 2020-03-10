using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count_Down : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 10f;

    public float CurrentTime => currentTime;

    void Start()
    {
        currentTime = startingTime; 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime; 
        
    }
}
