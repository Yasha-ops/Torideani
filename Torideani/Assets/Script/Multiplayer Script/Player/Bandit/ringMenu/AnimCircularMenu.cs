using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimCircularMenu : MonoBehaviour
{
    public List<ringMenu.MenuButton> buttons = new List<ringMenu.MenuButton>();
    private Vector2 Mouseposition;
    private Vector2 fromVectore2M = new Vector2(0.5f, 1.0f);
    private Vector2 centercirce = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    private Animator Anim;

    public GameObject ringMenuUI;

    public int menuItems;
    public int curMenuItem;
    private int OldMenueItem;

    ringMenu ringMenu;

    // Start is called before the first frame update
    void Start()
    {
        ringMenu = FindObjectOfType<ringMenu>();
        Anim = GetComponent<Animator>();
        menuItems = buttons.Count;
        foreach(ringMenu.MenuButton button in buttons)
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            setfalse();
        }
        if (ringMenu.RingAnimation)
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

        curMenuItem = (int) (angle / (360 / menuItems));

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
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("swimming", true);
       }
        if (curMenuItem == 1)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("belly", true);
        }
        if (curMenuItem == 2)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("breakdance", true);
        }
        if (curMenuItem == 3)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("jazz", true);
        }
        if (curMenuItem == 4)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("hiphop", true);
        }
        if (curMenuItem == 5)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("swing", true);
        }
        if (curMenuItem == 6)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("sittingyell", true);
        }
        if (curMenuItem == 7)
        {
            setfalse();
            Anim.SetTrigger("enter");
            Anim.SetBool("lay", true);
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


  
}
