using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouverture2 : MonoBehaviour
{
    public GameObject door;
    private bool isOpen = false;
    // Start is called before the first frame update
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
        if (transform.position.x > 5.0f && transform.position.y > 1f && transform.position.z < -14f)
        {
            Debug.Log("Openning the door !");
            OpenDoor();
        }
        else
        {
            Debug.Log($"My coordonates are  : {transform.position.x} X , {transform.position.y} Y, {transform.position.z}");

            if (isOpen)
                CloseDoor();
        }
    }

    public void CloseDoor()
    {
        door.transform.rotation = Quaternion.Euler(0f,0f,0f);
    }
}
