using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bonusspin : MonoBehaviour
{
    private bool up = true;
    private bool down = false;
    public float speed = 0.0015f;
    public float spin = 1.5f;
    public float spine;
    public Transform clok;


    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.y <= 1f)
        {
            up = true;
            down = false;
        }
        else if (transform.position.y >= 1.25f)
        {
            up = false;
            down = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (up && transform.position.y < 1.25f)
        {
            transform.position += new Vector3(0, speed, 0);
        }
        else
        {
            up = false;
            down = true;
        }

        if (down && transform.position.y > 1f)
        {
            transform.position -= new Vector3(0,speed, 0);
        }
        else
        {
            up = true;
            down = false;
        }

        clok.localEulerAngles = new Vector3(0, spine, 0);

        spine += spin;

    }
}
