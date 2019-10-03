using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiManager : MonoBehaviour
{
    public AudioSource effects;
    public AudioSource music;
    public static AudiManager instance;

    public void PlaySingle(AudioClip clip)
    {
        effects.clip = clip;
        effects.Play();
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    public void ChangeSong(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }
}
