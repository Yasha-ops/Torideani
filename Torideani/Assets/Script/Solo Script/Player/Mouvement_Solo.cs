using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class Mouvement_Solo : MonoBehaviourPun
{
    private const float Y_ANGLE_MIN = -3.0f;
    private const float Y_ANGLE_MAX = 3.0f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float currentY = 0.0f;
    private float mouseSensitivity = 100f;
    public Transform camTransform;

    public AudioSource audio;
    public AudioClip[] clips;

    private float fireDuration = 1.70f;
    private float currentFireDuration = 0f;

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

    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter
    private float ShakeElapsedTime = 0f;

    private bool death = false;

    public Vector3 ground;
    public Text Wasted_Text;

    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public GameObject cameraVise;
    public Transform gun;

    private void Update()
    {
        if (GetComponent<Solo_Class>().Health > 0)
        {
            TakeInput();
            BasicRotation();
            Cursor.lockState = CursorLockMode.Locked;

            Anim.SetFloat("direction", Input.GetAxis("Horizontal"));
            Anim.SetBool("Ground", isGrounded);

            currentFireDuration -= Time.deltaTime;

            if (Input.GetButtonDown("Fire1") && currentFireDuration <= 0) // Tirer
            {
                if (!this.GetComponent<Solo_Class>().isShootPossible)
                    return;
                this.gameObject.GetComponent<Solo_Class>().TakeInput();
                Anim.SetTrigger("shoot");
                ShakeElapsedTime = ShakeDuration;
                audio.clip = clips[0];
                audio.Play();
                this.gameObject.GetComponent<Solo_Class>().feu.Play();
                currentFireDuration = fireDuration;
            }
            if (Input.GetMouseButton(1)) // Viser
            {
                Anim.SetBool("aim", true);
                cameraVise.gameObject.SetActive(true);
                speed = 4f;
            }

            if (Input.GetMouseButtonUp(1))
            {
                Anim.SetBool("aim", false);
                speed = 5f;
                cameraVise.gameObject.SetActive(false);
            }
            if (Input.GetKey(KeyCode.End))
            {
                SceneManager.LoadScene("MainMenu");
            }

            if (VirtualCamera != null && virtualCameraNoise != null)
            {
                // If Camera Shake effect is still playing
                if (ShakeElapsedTime > 0)
                {
                    // Set Cinemachine Camera Noise parameters
                    virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                    virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                    // Update Shake Timer
                    ShakeElapsedTime -= Time.deltaTime;
                }
                else
                {
                    // If Camera Shake effect is over, reset variables
                    virtualCameraNoise.m_AmplitudeGain = 0f;
                    ShakeElapsedTime = 0f;
                }
            }
        }
        else
        {
            if (!death)
            {
                Wasted_Text.text = "WASTED";
                Anim.SetTrigger("death");
                death = true;
            }
            
        }

        if (Input.GetButton("Cancel") || Input.GetKey("escape"))
            Application.Quit();
        if (Input.GetKey("m"))
        {
            MainMenu.Disconect();
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
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

        if (Input.GetMouseButton(1)) // Viser
        {
            gun.localRotation = Quaternion.Euler(1, 241 - currentY, 272);
        }
        else
        {
            gun.localRotation = Quaternion.Euler(1, 241, 272);
        }

    }


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

        if (Input.GetButton("Fire3")) // Courir
        {
            move = transform.right * x + transform.forward * z;
            Anim.SetFloat("Speed", z * speed / 5);
            Anim.SetBool("Fire1", true);
        }
        else
        {
            move = transform.right * x / 2 + transform.forward * z / 2;
            Anim.SetFloat("Speed", z * speed / 10);
            Anim.SetBool("Fire1", false);
        }


        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
