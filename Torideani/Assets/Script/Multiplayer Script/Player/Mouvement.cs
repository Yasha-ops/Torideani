using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // ringMenus
    private Vector2 Mouseposition;
    private Vector2 fromVectore2M = new Vector2(0.5f, 1.0f);
    private Vector2 centercirce = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;
    private bool RingAnimation;
    private bool RingSong;
    private float angle;


    //ringMenu
    public List<MenuButton> buttonsMenu = new List<MenuButton>();
    private int menuItemsMenu;
    private int curMenuItemMenu;
    private int OldMenueItemMenu;
    public GameObject ringMenuUIMenu;

    //ringAnimation
    public List<MenuButton> buttonsAnimation = new List<MenuButton>();
    private int menuItemsAnimation;
    private int curMenuItemAnimation;
    private int OldMenueItemAnimation;
    public GameObject ringMenuUIAnimation;

    //ringSong
    public List<MenuButton> buttonsSong = new List<MenuButton>();
    private int menuItemsSong;
    private int curMenuItemSong;
    private int OldMenueItemSong;
    public GameObject ringMenuUISong; 
    public AudioClip[] bruitage;
    private AudioSource audioSource;

    public bool isShootPossible = true;
    public GameObject Aim;
    public GameObject CamerePosition;

    #endregion

    private void Start()
    {
        if (photonView.IsMine)
        {
            //cameraParent.SetActive(true);
            Anim = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            startRingMenu();
            startRingAnimation();
            startRingSong();
        }
    }

    private void Update()
    {
        if (photonView.IsMine && !bonus_locked)
        {
            TakeInput();
            BasicRotation();
            Anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
            if (this.gameObject.tag == "Chasseur")
            {
                GameObject yt = GameObject.FindWithTag("GameSetup");
                if (Input.GetButton("Fire2"))
                {
                    speed = 3f;
                    yt.GetComponent<GameSetup>().CameraAim.gameObject.SetActive(true);
                    Anim.SetBool("aim", true);
                }
                else
                {
                    speed = 6f;
                    yt.GetComponent<GameSetup>().CameraAim.gameObject.SetActive(false);
                    Anim.SetBool("aim", false);
                }
            }
            if (this.gameObject.tag == "Bandit")
            {
                updateMenuAnimation();
                updateSong(); 
                updateMenu();
            }
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
        Anim.SetBool("Ground", isGrounded);
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

        if (Input.GetButton("Fire3"))
        {
            move = transform.right * x  + transform.forward * z ;
            Anim.SetFloat("Speed", z * speed / 6);
            Anim.SetBool("Fire1", true);
        }
        else
        {
            move = transform.right * x /2+ transform.forward * z /2;
            Anim.SetFloat("Speed", z * speed / 12);
            Anim.SetBool("Fire1", false); 
        }


        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void BasicRotation()
    {
        if (this.gameObject.tag != "Bandit" || (!Input.GetKey("x") && !Input.GetButton("Fire2")))
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

    private void startRingMenu()
    {
        RingAnimation = false;
        RingSong = false;
        menuItemsMenu = buttonsMenu.Count;
        foreach (MenuButton button in buttonsMenu)
        {
            button.sceneimage.color = button.normalColor;
        }
        curMenuItemMenu = 0;
        OldMenueItemMenu = 0;
        ringMenuUIMenu.SetActive(false);
    }

    private void startRingAnimation()
    {
        menuItemsAnimation = buttonsAnimation.Count;
        foreach (MenuButton button in buttonsAnimation)
        {
            button.sceneimage.color = button.normalColor;
        }
        curMenuItemAnimation = 0;
        OldMenueItemAnimation = 0;
        ringMenuUIAnimation.SetActive(false);
    }

    private void startRingSong()
    {
        audioSource = GetComponent<AudioSource>();
        menuItemsSong = buttonsSong.Count;
        foreach (MenuButton button in buttonsSong)
        {
            button.sceneimage.color = button.normalColor;
        }
        curMenuItemSong = 0;
        OldMenueItemSong = 0;
        ringMenuUISong.SetActive(false);
    }

    private void updateMenu()
    {
        if (!Input.GetKey("x"))
        {
            ringMenuUIMenu.SetActive(false);
            RingSong = false;
            RingAnimation = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!RingAnimation && !RingSong)
        {
            Cursor.lockState = CursorLockMode.None;
            ringMenuUIMenu.SetActive(true);
            GetCurrentMenuItemMenu();
            if (Input.GetButtonDown("Fire1"))
                ButtonAcionMenu();
        }
        else if (RingAnimation || RingSong)
        {
            ringMenuUIMenu.SetActive(false);
        }
    }

    private void updateMenuAnimation()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            setfalse();
        }
        if (RingAnimation)
        {
            Cursor.lockState = CursorLockMode.None;
            ringMenuUIAnimation.SetActive(true);
            GetCurrentMenuItemAnimation();
            if (Input.GetButtonDown("Fire1"))
                ButtonAcionAnimation();
        }
        else
        {
            ringMenuUIAnimation.SetActive(false);
        }

    }

    private void updateSong()
    {
        if (RingSong)
        {
            Cursor.lockState = CursorLockMode.None;
            ringMenuUISong.SetActive(true);
            GetCurrentMenuItemSong();
            if (Input.GetButtonDown("Fire1"))
                ButtonAcionSong();
        }
        else
        {
            ringMenuUISong.SetActive(false);
        }
    }

    private void updateAngle()
    {
        Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);

        angle = (Mathf.Atan2(fromVectore2M.y - centercirce.y, fromVectore2M.x - centercirce.x) - Mathf.Atan2(toVector2M.y - centercirce.y, toVector2M.x - centercirce.x)) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;
    }

    public void GetCurrentMenuItemMenu()
    {
        updateAngle();

        curMenuItemMenu = (int)(angle / (360 / menuItemsMenu));

        if (curMenuItemMenu == OldMenueItemMenu)
            return;
        buttonsMenu[OldMenueItemMenu].sceneimage.color = buttonsMenu[OldMenueItemMenu].normalColor;
        OldMenueItemMenu = curMenuItemMenu;
        buttonsMenu[curMenuItemMenu].sceneimage.color = buttonsMenu[curMenuItemMenu].HighlitedColor;
    }

    public void GetCurrentMenuItemAnimation()
    {
        updateAngle();
        curMenuItemAnimation = (int)(angle / (360 / menuItemsAnimation));

        if (curMenuItemAnimation == OldMenueItemAnimation)
            return;
        buttonsAnimation[OldMenueItemAnimation].sceneimage.color = buttonsAnimation[OldMenueItemAnimation].normalColor;
        OldMenueItemAnimation = curMenuItemAnimation;
        buttonsAnimation[curMenuItemAnimation].sceneimage.color = buttonsAnimation[curMenuItemAnimation].HighlitedColor;
    }

    public void GetCurrentMenuItemSong()
    {
        updateAngle();

        curMenuItemSong = (int)(angle / (360 / menuItemsSong));

        if (curMenuItemSong == OldMenueItemSong)
            return;
        buttonsSong[OldMenueItemSong].sceneimage.color = buttonsSong[OldMenueItemSong].normalColor;
        OldMenueItemSong = curMenuItemSong;
        buttonsSong[curMenuItemSong].sceneimage.color = buttonsSong[curMenuItemSong].HighlitedColor;
    }

    public void ButtonAcionMenu()
    {
        buttonsMenu[curMenuItemMenu].sceneimage.color = buttonsMenu[curMenuItemMenu].PressedColor;
        if (curMenuItemMenu == 0)
        {
            RingAnimation = true;
        }
        if (curMenuItemMenu == 1)
        {
            RingSong = true;
        }
    }

    public void ButtonAcionAnimation() // On peut remplacer ca par une liste !
    {
        buttonsAnimation[curMenuItemAnimation].sceneimage.color = buttonsAnimation[curMenuItemAnimation].PressedColor;
        setfalse();
        Anim.SetTrigger("enter");
        switch (curMenuItemAnimation)
        {
            case 0:
                Anim.SetBool("swimming", true);
                break;
            case 1:
                Anim.SetBool("belly", true);
                break;
            case 2:
                Anim.SetBool("breakdance", true);
                break;
            case 3:
                Anim.SetBool("jazz", true);
                break;
            case 4:
                Anim.SetBool("hiphop", true);
                break;
            case 5:
                Anim.SetBool("swing", true);
                break;
            case 6:
                Anim.SetBool("sittingyell", true);
                break;
            case 7:
                Anim.SetBool("lay", true);
                break;
            default:
                break;
        }
    }

    public void ButtonAcionSong()
    {
        buttonsSong[curMenuItemSong].sceneimage.color = buttonsSong[curMenuItemSong].PressedColor;
        if (curMenuItemSong != 7)
        {
            audioSource.PlayOneShot(bruitage[curMenuItemSong]);
        }
        else
        {
            audioSource.PlayOneShot(bruitage[UnityEngine.Random.Range(0, bruitage.Length)]);
        }
    }

    public void setfalse()
    {
        Anim.SetBool("swing", false);
        Anim.SetBool("belly", false);
        Anim.SetBool("swimming", false);
        Anim.SetBool("breakdance", false);
        Anim.SetBool("jazz", false);
        Anim.SetBool("hiphop", false);
        Anim.SetBool("sittingyell", false);
        Anim.SetBool("lay", false);
    }

    [System.Serializable]
    public class MenuButton
    {
        public string name;
        public Image sceneimage;
        public Color normalColor = Color.white;
        public Color HighlitedColor = Color.grey;
        public Color PressedColor = Color.gray;
    }
}
