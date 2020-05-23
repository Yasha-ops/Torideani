using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curseur : MonoBehaviour
{
    public GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        music.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
