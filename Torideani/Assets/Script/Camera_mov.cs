using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Camera_mov : MonoBehaviourPun
{

    public Camera entityCamera;

    private string username;

    #region Old_Code

    /*// Start is called before the first frame update
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    private float xRotation = 0f;
    //private float yRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }
    }


    private void TakeInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        
        //yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); 
        //playerBody.Rotate(Vector3.left * mouseY); 
    }*/

    #endregion
}
