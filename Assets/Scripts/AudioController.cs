using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] sources;

    private void Awake()
    {
        sources = GetComponents<AudioSource>();
    }
    
    // This method is used to check if there are other clips playing and if there are none then it plays the clip we pass as argument
    public void PlayClip(AudioClip clip)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            if (!sources[i].isPlaying)
            {
                sources[i].clip = clip;
                sources[i].Play();
                break;
            }
        }
    }
}
