using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera_MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+0.5f , 280f, -9f); 
    }
}
