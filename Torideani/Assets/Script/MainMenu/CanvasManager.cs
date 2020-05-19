using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas;
    public CinemachineVirtualCamera currentCamera;
    private CinemachineTrackedDolly cinemachineTrackedDolly;
void Start()
    {
        cinemachineTrackedDolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    void Update()
    {
        if (cinemachineTrackedDolly.m_PathPosition == 7)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
