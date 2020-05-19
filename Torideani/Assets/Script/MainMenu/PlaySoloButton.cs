using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoloButton : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] adClips;
    AudioSource adSource;

    void Start()
    {
        adSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        adSource.clip = adClips[1];
        adSource.Play();
        this.GetComponent<Ouverture3>().OpenDoor();

    }

    void OnMouseEnter()
    {
        adSource.clip = adClips[0];
        adSource.Play();
    }

}
