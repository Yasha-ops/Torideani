using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Mouvement_Solo : MonoBehaviourPun
{
    private const float Y_ANGLE_MIN = -15.0f;
    private const float Y_ANGLE_MAX = 15.0f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float currentY = 0.0f;
    private float mouseSensitivity = 100f;
    public Transform camTransform;

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

    public Vector3 ground;

    public GameObject cameraVise;

    private void Update()
    {
        TakeInput();
        BasicRotation();
        Cursor.lockState = CursorLockMode.Locked;
        Anim.SetFloat("direction", Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Fire1")) // Tirer
        {
            if (!this.GetComponent<Solo_Class>().isShootPossible)
                return;
            this.gameObject.GetComponent<Solo_Class>().TakeInput();
            Anim.SetTrigger("shoot");
            this.gameObject.GetComponent<Solo_Class>().feu.Play();
        }
        if (Input.GetMouseButton(1)) // Viser
        {
            cameraVise.gameObject.SetActive(true);
            speed = 2f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            speed = 6f;
            cameraVise.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.End))
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

        currentY -= Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -20f, 20f);


        transform.Rotate(Vector3.up * mouseX);

        camTransform.localRotation = Quaternion.Euler(currentY, 0, 0);

    }


    private void TakeInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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
        if (Input.GetKey(KeyCode.End))
        {
            MainMenu.Disconect();
            SceneManager.LoadScene("MainMenu");
        }

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

        if (Input.GetButton("Fire3")) // Courir
        {
            move = transform.right * x + transform.forward * z;
            Anim.SetFloat("Speed", z);
            Anim.SetBool("Fire1", true);
        }
        else
        {
            move = transform.right * x / 2 + transform.forward * z / 2;
            Anim.SetFloat("Speed", z / 2);
            Anim.SetBool("Fire1", false);
        }


        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
