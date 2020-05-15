using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas;
    
    void Update()
    {

        if (transform.position == new Vector3(17.32976f, 4.283929f, -12.19154f))
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log($"My coordonates are  : {transform.position.x} X , {transform.position.y} Y, {transform.position.z}");
            canvas.gameObject.SetActive(false);
        }
    }
}
