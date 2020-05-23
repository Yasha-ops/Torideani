using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class option : MonoBehaviour
{
    public AudioMixer audioMixer;
    public float vol;
    public void SetVolume (float volume)
    {

        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }
}
