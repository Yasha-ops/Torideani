using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Train : MonoBehaviour
{
    public GameObject gamesetup;
    public float speed = 0.25f;    
    public float doorspeed = 0.1f;
    public Transform door1;
    public Transform door2;
    public Transform door1_2;
    public Transform door2_2;
    public Transform rou1;
    public Transform rou2;
    public Transform rou3;
    public Transform rou4;
    private float rota = 0f;
    public ParticleSystem smook;
    private bool smok = true;

    private float timeClose = 40f;
    private bool isOpen = false;

    private float acc;
    void Update()
    {
        acc += Time.deltaTime;
        Debug.Log(acc);
        if (acc > gamesetup.GetComponent<GameSetup>().GameDuration - 60f && acc < gamesetup.GetComponent<GameSetup>().GameDuration + 45f)
        {
            if (smok)
            {
                smook.Play();
                smok = false;
            }
            if (transform.position.x < -30f)
            { 
                transform.position += new Vector3(speed, 0, 0);
                rou1.localRotation = Quaternion.Euler(rota, 0, 0);
                rou2.localRotation = Quaternion.Euler(rota , 0 ,0);
                rou3.localRotation = Quaternion.Euler(rota, 0, 0);
                rou4.localRotation = Quaternion.Euler(rota, 0, 0);
                rota += speed * 30;
            }
            else if (transform.position.x >= -30f && transform.position.x < 23f)
            {
                transform.position += new Vector3(speed / ((transform.position.x + 37.1f) / 6) , 0, 0);
                rou1.localRotation = Quaternion.Euler(rota, 0, 0);
                rou2.localRotation = Quaternion.Euler(rota, 0, 0);
                rou3.localRotation = Quaternion.Euler(rota, 0, 0);
                rou4.localRotation = Quaternion.Euler(rota, 0, 0);
                rota += speed / ((transform.position.x + 37.1f) / 6) * 30;
            }
            else if (transform.position.x >= 23f && door1.position.x < 13f)
            {
                door1.position += new Vector3(doorspeed, 0, 0);
                door2.position -= new Vector3(doorspeed, 0, 0);
                door1_2.position += new Vector3(doorspeed, 0, 0);
                door2_2.position -= new Vector3(doorspeed, 0, 0);
            }
        }
        else if (acc >= gamesetup.GetComponent<GameSetup>().GameDuration + 45f)
        {
            Debug.Log("cas2");
            if (door1.position.x > 11.6f)
            {
                door1.position -= new Vector3(doorspeed, 0, 0);
                door2.position += new Vector3(doorspeed, 0, 0);
                door1_2.position -= new Vector3(doorspeed, 0, 0);
                door2_2.position += new Vector3(doorspeed, 0, 0);
            }
        }
    }
}
