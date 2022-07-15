using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public List<AudioSource> audiosources;
    public static SoundManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySound(int index)
    {
        if (!audiosources[index].isPlaying)
            audiosources[index].Play();

    }

}
