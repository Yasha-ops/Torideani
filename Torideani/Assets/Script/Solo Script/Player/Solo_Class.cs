﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Solo_Class : MonoBehaviour
{
    private float health;
    private int ammo;
    private float damage;
    private int score;
    public Transform rayOrigin;
    public ParticleSystem feu; 
    public Text Score_Text;
    void Start()
    {
        health = 20;
        ammo = 120;
    }

    public void TakeInput()
    {
        if (ammo > 0)
        {
            Debug.Log($"U have {ammo} left");
            Shoots();
            ammo--;
        }
    }

    public void Shoots()
    {
        feu.Play();
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 100))
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, Color.white);
            Debug.Log("Did Hit"); 
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Tete")
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                        Color.red); 
                score += 100;
                Debug.Log($"U have {score}");
                Score_Text.text = $"U have {score}";
                hit.transform.gameObject.GetComponent<AI_Class>().TakeDamage(damage);
            }
            if (hit.transform.tag == "Buste")
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                        Color.red); 
                score += 50;
                Debug.Log($"U have {score}");
                Score_Text.text = $"U have {score}";
                hit.transform.gameObject.GetComponent<AI_Class>().TakeDamage(damage);
            }
            if (hit.transform.tag == "Jambe")
            {
                Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                        Color.red); 
                score += 25;
                Debug.Log($"U have {score}");
                Score_Text.text = $"U have {score}";
                hit.transform.gameObject.GetComponent<AI_Class>().TakeDamage(damage);
            }

        }
        else 
        { 
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 100, 
                    Color.yellow); 
            Debug.Log("Did not Hit");
        } 
    }



    public void TakeDamage()
    {
        health -= 0.25f;
    }
}