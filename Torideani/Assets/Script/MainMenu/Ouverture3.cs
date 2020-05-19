using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ouverture3 : MonoBehaviour
{
    public GameObject door;
    private bool acivate = false;
    private float speed = 25.0f;
    
    void Update()
    {
        if (Convert.ToInt32(door.transform.rotation.y * 100f) != 99  && acivate)
        {
            door.transform.Rotate(- Vector3.up * speed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        acivate = true;
    }
}
