using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip birdFlySound,bigBirdSound,smallBirdSound,clickSound,levelCompleteSfx,sortedSfx;
    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

   public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
