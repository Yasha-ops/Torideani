using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Camera_mov : MonoBehaviourPun
{
   private float xRotation = 0f;
   private float yRotation = 0f;
   private float mouseSensitivity = 100;
   private Animator anime;
   private void Update()
   {
      BasicRotation();
      Cursor.lockState = CursorLockMode.Locked;
      if (Input.GetButton("Fire1"))
      {
         anime.SetTrigger("shoot");
      }
   }

   private void Start()
   {
      anime = GetComponent<Animator>(); 
      
   }

   private void BasicRotation()
   {
      float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

      xRotation -= mouseY;
      xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        
      yRotation += mouseX;
         
      transform.Rotate(Vector3.up * mouseX);
   }
}
