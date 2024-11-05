using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSBManager : MonoBehaviour
{
    public static SoundSBManager Instance;

    public AudioClip betSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip noneSound;
    public AudioClip confirmSound;
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

    public void PlayBetSound()
    {
        audioSource.PlayOneShot(betSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }

    public void PlayNoneBet()
    {
        audioSource.PlayOneShot(noneSound);
    }
    public void PlayConfirmSound()
    {
        audioSource.PlayOneShot(confirmSound);
    }
}
