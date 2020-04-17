using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    private RaycastHit Hit;
    private CharacterController controller = null;
    public bool isGrounded;
    private Animator Anim;
    public Transform groundCheck;
    public float groundDistance = 0f;
    public LayerMask groundMask;
    public float speed = 1f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        animation();
    }

    private void move()
    {
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 2))
        {
            transform.Rotate(Vector3.up * Random.Range(50, 200));
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


     private void animation()
    {
        if (!isGrounded)
        {
            Anim.SetBool("Ground", true);
        }
        else
        {
            Anim.SetBool("Ground", false);
        }
        Anim.SetFloat("Speed", 0.4f);
        Anim.SetFloat("Direction", Input.GetAxis("Horizontal"));

    }
 }
