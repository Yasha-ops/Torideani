using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouverture : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    // Start is called before the first frame update
    public void OpenDoor()
    {
        float test = 0f;
        while(test < 130f)
        {
            test += 0.001f;
            rightDoor.transform.rotation = Quaternion.Euler(0f,test,0f);
            leftDoor.transform.rotation = Quaternion.Euler(0f,-test,0f);
        }
    }

    public void CloseDoor()
    {
        rightDoor.transform.rotation = Quaternion.Euler(0f,0f,0f);
        leftDoor.transform.rotation = Quaternion.Euler(0f,0f,0f);
    }
}
