using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class timeur : MonoBehaviour
{
    private Animator Anim;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        time = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0f)
        {
            Anim.SetTrigger("punch");
            time = 4f;
        }
        time -= Time.deltaTime;
    }
}
