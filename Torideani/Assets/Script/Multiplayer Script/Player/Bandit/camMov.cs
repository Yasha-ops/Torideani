using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMov : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -25.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    private float distance = 4f;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    private Vector3 camPositionStart;
    private Quaternion camRotationStart;
    public float X = 0.0f;
    public float Y = 0.0f;


    private float xRotation = 0f;

    private float mouseSensitivity = 100;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        camPositionStart = camTransform.localPosition;
        camRotationStart = camTransform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Bandit")
        {
            if (Input.GetButton("Fire2"))
            {
                currentX += Input.GetAxis("Mouse X");
                currentY -= Input.GetAxis("Mouse Y");
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
            }


            X = Input.GetAxis("Mouse X");
            Y = Input.GetAxis("Mouse Y");
        }
        

    }

    void LateUpdate()
    {
        if (this.gameObject.tag == "Bandit")
        {
            if (Input.GetButton("Fire2"))
            {
                Vector3 dir = new Vector3(0, 2, -3.5f);
                Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
                camTransform.position = lookAt.position + rotation * dir;
                camTransform.LookAt(lookAt.position);
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                camTransform.localPosition = camPositionStart;
                camTransform.localRotation = camRotationStart;
            }
        }
    }
}
