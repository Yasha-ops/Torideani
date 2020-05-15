using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Ouverture2 : MonoBehaviour
{
    public GameObject door;
    public GameObject canvas;
    private bool isOpen = false;
    public CinemachineVirtualCamera currentCamera;
    private CinemachineTrackedDolly cinemachineTrackedDolly;


    void Start()
    {
        cinemachineTrackedDolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly> ();
    }
    public void OpenDoor()
    {
        float test = 0f;
        while(test < 145f)
        {
            test += 0.001f;
            door.transform.rotation = Quaternion.Euler(0f,test,0f);
        }
        isOpen = true;
    }

    void Update()
    {
        Debug.Log(cinemachineTrackedDolly.m_PathPosition);
        if (cinemachineTrackedDolly.m_PathPosition == 10 )
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            if (transform.position.x > 5.0f && transform.position.y > 1f && transform.position.z < -14f)
            {
                Debug.Log("Openning the door !");
                OpenDoor();
            }
            else
            {
                if (isOpen)
                    CloseDoor();
            }
        }
    }

    public void CloseDoor()
    {
        door.transform.rotation = Quaternion.Euler(0f,0f,0f);
    }
}
