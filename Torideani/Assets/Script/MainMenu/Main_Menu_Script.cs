using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)); 
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Solo()
    {
        SceneManager.LoadScene("Solo_Game");
    }
    
    

}
