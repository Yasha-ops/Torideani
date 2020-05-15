using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BackMultiplayer: MonoBehaviour
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
            this.GetComponent<Ouverture>().CloseDoor();
        }
        catch
        {
        }
        transition.gameObject.SetActive(false);

    }

    void OnMouseEnter()
    {
        adSource.clip = adClips[0];
        adSource.Play();
    }
}
