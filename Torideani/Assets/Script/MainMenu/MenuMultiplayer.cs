using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuMultiplayer: MonoBehaviour
{
    public GameObject transition;
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
        try
        {
            this.GetComponent<Ouverture>().OpenDoor();
        }
        catch
        {
        }
        transition.gameObject.SetActive(true);

    }

    void OnMouseEnter()
    {
        adSource.clip = adClips[0];
        adSource.Play();
    }
}
