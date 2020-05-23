using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Ouverture3 : MonoBehaviour
{
    public GameObject door;
    private bool acivate = false;
    private float speed = 25.0f;
    public GameObject cam;

    void Update()
    {
        if (Convert.ToInt32(door.transform.rotation.y * 100f) != 99  && acivate)
        {
            door.transform.Rotate(- Vector3.up * speed * Time.deltaTime); 
            cam.gameObject.GetComponent<PostProcessVolume>().enabled = true;
        }

        if (Convert.ToInt32(door.transform.rotation.y * 100f) == 99)
            SceneManager.LoadScene("Solo_Game");
       
    }

    public void OpenDoor()
    {
        acivate = true;
    }
}
