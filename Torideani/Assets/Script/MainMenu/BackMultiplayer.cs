using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BackMultiplayer: MonoBehaviour
{
    public GameObject transition;
    public AudioClip[] adClips;
    AudioSource adSource;
    public GameObject canvas;

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
            this.GetComponent<Ouverture>().CloseDoor();
        }
        catch
        {
        }

        try{
            canvas.gameObject.SetActive(false);
        }catch{
        }
        transition.gameObject.SetActive(false);

    }

    void OnMouseEnter()
    {
        adSource.clip = adClips[0];
        adSource.Play();
    }
}
