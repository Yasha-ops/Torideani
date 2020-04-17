using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class Mouvement : MonoBehaviourPun
{
#region Constantes
    [SerializeField] private float movementSpeed = 0f;
    private CharacterController controller = null;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera mainCamera;
    public GameObject cameraParent; 
    private Animator Anim;
    public bool bonus_locked = false;
    public bool bonus_jump; 



#endregion

    private void Start()
    {
        if (photonView.IsMine)
        {
            cameraParent.SetActive(true);
            Anim = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (photonView.IsMine && !bonus_locked)
        {
            TakeInput();
            BasicRotation();
            Anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
        }
    }
#region Constantes
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private float xRotation = 0f;
    public Transform groundCheck;
    public float groundDistance = 0f;
    public LayerMask groundMask;
    private float mouseSensitivity = 100; 

    private Vector3 velocity;
#endregion

    public bool isGrounded;

    private void TakeInput()
    {
        if (isGrounded)
        {
            Anim.SetBool("Ground", true);
        }
        else
        {
            Anim.SetBool("Ground", false);
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
        if (!Input.GetKey("x") && !Input.GetKey("v") && !Input.GetButton("Fire2"))
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


    public void ChangeBonusValue(string bonuus)
    {
        photonView.RPC("RPC_BONUS", RpcTarget.All ,bonuus);
        Debug.Log("You are currently locked !");
    }


    [PunRPC]
        void RPC_BONUS(string bonuus)
        {
            switch(bonuus)
            {
                case "Locked": 
                    bonus_locked = true;
                    break; 
            }
        }
}
