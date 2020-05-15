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
        Debug.Log("A mouse click has been detected !");
        adSource.clip = adClips[1];
        adSource.Play();
        try
        {
            this.GetComponent<Ouverture>().OpenDoor();
        }
        catch
        {
            Debug.Log("It seems that ur trying something else !");
        }
        transition.gameObject.SetActive(true);

    }

    void OnMouseEnter()
    {
        adSource.clip = adClips[0];
        adSource.Play();
    }
}
