using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouvement_Solo : MonoBehaviourPun
{
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float mouseSensitivity = 100f;
    public Transform effect;
    public Transform groundCheck;
    private Animator Anim;
    public bool isGrounded;
    public float groundDistance = 0f;
    public LayerMask groundMask;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private Vector3 velocity;
    public CharacterController controller;

    private void Update()
    {
        BasicRotation();
        Cursor.lockState = CursorLockMode.Locked;
        TakeInput();
        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.GetComponent<Solo_Class>().TakeInput();
            Anim.SetTrigger("shoot");
        }
        if (Input.GetKey("m"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -20f, 20f);

        //transform.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }


    private void TakeInput()
    {
        if (isGrounded)
        {
            //Anim.SetBool("Ground", true);
        }
        else
        {
            //Anim.SetBool("Ground", false);
        }

        if (Input.GetButton("Cancel") || Input.GetKey("escape"))
            Application.Quit();
        if (Input.GetKey("m"))
        {
            MainMenu.Disconect();
            SceneManager.LoadScene("MainMenu");
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            //Anim.SetTrigger("Jumpstart");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        Vector3 move;

        if (Input.GetButton("Fire1"))
        {
            move = transform.right * x  + transform.forward * z ;
            //Anim.SetFloat("Speed", z);
            //Anim.SetBool("Fire1", true);
        }
        else
        {
            move = transform.right * x /2+ transform.forward * z /2;
            //Anim.SetFloat("Speed", z/2);
            //Anim.SetBool("Fire1", false); 
        }


        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
