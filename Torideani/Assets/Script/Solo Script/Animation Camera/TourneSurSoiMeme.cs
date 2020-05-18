using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TourneSurSoiMeme : MonoBehaviour
{
    public float speed = 50.0f;
    
    void Update()
    {
        this.transform.Rotate(- Vector3.up * speed * Time.deltaTime);
    }
}
