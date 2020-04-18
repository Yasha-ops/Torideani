using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongCircularMenu : MonoBehaviour
{
    public List<ringMenu.MenuButton> buttons = new List<ringMenu.MenuButton>();
    private Vector2 Mouseposition;
    private Vector2 fromVectore2M = new Vector2(0.5f, 1.0f);
    private Vector2 centercirce = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public AudioClip[] bruitage;
    private AudioSource audioSource;
    

    public GameObject ringMenuUI;

    public int menuItems;
    public int curMenuItem;
    private int OldMenueItem;

    ringMenu ringMenu;

    // Start is called before the first frame update
    void Start()
    {
        ringMenu = FindObjectOfType<ringMenu>();
        audioSource = GetComponent<AudioSource>();
        menuItems = buttons.Count;
        foreach (ringMenu.MenuButton button in buttons)
        {
            button.sceneimage.color = button.normalColor;
        }
        curMenuItem = 0;
        OldMenueItem = 0;
        ringMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (ringMenu.RingSong)
        {
            Cursor.lockState = CursorLockMode.None;
            ringMenuUI.SetActive(true);
            GetCurrentMenuItem();
            if (Input.GetButtonDown("Fire1"))
                ButtonAcion();
        }
        else
        {
            ringMenuUI.SetActive(false);
        }


    }

    public void GetCurrentMenuItem()
    {
        Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVectore2M.y - centercirce.y, fromVectore2M.x - centercirce.x) - Mathf.Atan2(toVector2M.y - centercirce.y, toVector2M.x - centercirce.x)) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        curMenuItem = (int)(angle / (360 / menuItems));

        if (curMenuItem != OldMenueItem)
        {
            buttons[OldMenueItem].sceneimage.color = buttons[OldMenueItem].normalColor;
            OldMenueItem = curMenuItem;
            buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].HighlitedColor;
        }
    }

    public void ButtonAcion()
    {
        buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].PressedColor;
        if (curMenuItem == 0)
        {
            audioSource.PlayOneShot (bruitage[0]);
        }
        if (curMenuItem == 1)
        {
            audioSource.PlayOneShot(bruitage[1]);
        }
        if (curMenuItem == 2)
        {
            audioSource.PlayOneShot(bruitage[2]);
        }
        if (curMenuItem == 3)
        {
            audioSource.PlayOneShot(bruitage[3]);
        }
        if (curMenuItem == 4)
        {
            audioSource.PlayOneShot(bruitage[4]);
        }
        if (curMenuItem == 5)
        {
            audioSource.PlayOneShot(bruitage[5]);
        }
        if (curMenuItem == 6)
        {
            audioSource.PlayOneShot(bruitage[6]);
        }
        if (curMenuItem == 7)
        {
            audioSource.PlayOneShot(bruitage[Random.Range(0, bruitage.Length)]);
        }
    }
}