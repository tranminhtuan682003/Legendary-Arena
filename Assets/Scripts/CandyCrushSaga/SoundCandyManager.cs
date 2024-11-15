using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCandyManager : MonoBehaviour
{
    public static SoundCandyManager Instance;

    public AudioClip swipe;
    public AudioClip butotnClick;
    public AudioClip destroy;
    public AudioClip point;
    public AudioClip music;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicSound();
    }

    public void PlaySwipeSound()
    {
        audioSource.PlayOneShot(swipe);
    }

    public void PlayPointSound()
    {
        audioSource.PlayOneShot(point);
    }

    public void PlayMusicSound()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayDestroySound()
    {
        audioSource.PlayOneShot(destroy);
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(butotnClick);
    }
}
