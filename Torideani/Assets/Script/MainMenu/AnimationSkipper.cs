using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AnimationSkipper : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    private CinemachineTrackedDolly cinemachineTrackedDolly;

    public int FinalPathNumber;
    
    // Start is called before the first frame update
    void Start()
    {
         cinemachineTrackedDolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (cinemachineTrackedDolly.m_PathPosition == FinalPathNumber)
        {
            cinemachineTrackedDolly.m_AutoDolly.m_Enabled = false;
            return;
        }
        if (Input.GetButton("Fire3") || Input.GetButton("Submit") )
        {
            cinemachineTrackedDolly.m_AutoDolly.m_Enabled = true;
        }
    }
}
