using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_mov : MonoBehaviourPun
{
   private float xRotation = 0f;
   private float yRotation = 0f;
   private float mouseSensitivity = 100;
   public Transform effect;
   private Animator anime;
   private void Update()
   {
      BasicRotation();
      Cursor.lockState = CursorLockMode.Locked;
      if (Input.GetButton("Fire1"))
      {
         anime.SetTrigger("shoot");
      }
      if (Input.GetKey("m"))
      {
            SceneManager.LoadScene("MainMenu");
      }
    }

   private void Start()
   {
      anime = GetComponent<Animator>();
      Cursor.lockState = CursorLockMode.Locked;

    }

   private void BasicRotation()
   {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        
      yRotation += mouseX;
      yRotation = Mathf.Clamp(yRotation, -45f, 45f);

        transform.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }
}
