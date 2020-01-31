using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Mouvement : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed = 0f;

    private CharacterController controller = null;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera mainCamera; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
            BasicRotation();
        }
    }

     public float speed = 12f;
     public float gravity = -9.81f;
     public float jumpHeight = 3f;
     private float xRotation = 0f;
     public Transform groundCheck;
     public float groundDistance = 0.4f;
     public LayerMask groundMask;
     private float mouseSensitivity = 100; 
     
     private Vector3 velocity;
     public bool isGrounded;
     private void TakeInput()
     {
         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
         if (isGrounded && velocity.y < 0)
         {
             velocity.y = -2f;
         }
         
         float x = Input.GetAxis("Horizontal");
         float z = Input.GetAxis("Vertical");
 
         if (Input.GetButtonDown("Jump") && isGrounded)
         {
             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         }
 
         Vector3 move = transform.right * x + transform.forward * z ;
 
         controller.Move(move * speed * Time.deltaTime);
 
         velocity.y += gravity * Time.deltaTime;
 
         controller.Move(velocity * Time.deltaTime); 
     }

     private void BasicRotation()
     {
         float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
         float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

         xRotation -= mouseY;
         xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        
         //yRotation += mouseX;
         
         transform.Rotate(Vector3.up * mouseX); 
         //playerBody.Rotate(Vector3.left * mouseY); 
     }
}
