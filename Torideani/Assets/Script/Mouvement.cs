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

    private Animator Anim;
    private void Start()
    {

        Anim = GetComponent<Animator>();
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

        Anim.SetFloat("Direction", Input.GetAxis("Horizontal"));

        if (isGrounded)
        {
            Anim.SetBool("Ground", true);
        }
        else
        {
            Anim.SetBool("Ground", false);
        }
    }

     public float speed = 6f;
     public float gravity = -9.81f;
     public float jumpHeight = 2f;
     private float xRotation = 0f;
     public Transform groundCheck;
     public float groundDistance = 0f;
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
 
         if (isGrounded && Input.GetButtonDown("Jump"))
         {
                Anim.SetTrigger("Jumpstart");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
          
         }

        Vector3 move;

         if (Input.GetButton("Fire1"))
         {
            move = transform.right * x  + transform.forward * z ;
            Anim.SetFloat("Speed", z);
            Anim.SetBool("Fire1", true);
        }
         else
         {
            move = transform.right * x /2+ transform.forward * z /2;
            Anim.SetFloat("Speed", z/2);
            Anim.SetBool("Fire1", false);
        }
         
 
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
