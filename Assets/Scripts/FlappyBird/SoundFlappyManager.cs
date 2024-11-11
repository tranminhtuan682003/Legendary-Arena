using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFlappyManager : MonoBehaviour
{
    public static SoundFlappyManager Instance;

    public AudioClip die;
    public AudioClip hit;
    public AudioClip point;
    public AudioClip swooshing;
    public AudioClip wing;
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

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(die);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hit);
    }

    public void PlaySwooshingSound()
    {
        audioSource.PlayOneShot(swooshing);
    }

    public void PlayWingBet()
    {
        audioSource.PlayOneShot(wing);
    }
    public void PlayMusicSound()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayPointSound()
    {
        audioSource.PlayOneShot(point);
    }
}
