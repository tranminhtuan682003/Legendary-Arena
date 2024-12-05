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

    public void IncreaseVolume()
    {
        if (audioSource.volume < 1.0f) // Kiểm tra nếu âm lượng nhỏ hơn mức tối đa
        {
            audioSource.volume += 0.1f; // Tăng âm lượng lên 0.1
            if (audioSource.volume > 1.0f)
            {
                audioSource.volume = 1.0f; // Đảm bảo không vượt quá 1.0
            }
        }
    }

    public void DownVolume()
    {
        if (audioSource.volume > 0.0f) // Kiểm tra nếu âm lượng lớn hơn mức tối thiểu
        {
            audioSource.volume -= 0.1f; // Giảm âm lượng đi 0.1
            if (audioSource.volume < 0.0f)
            {
                audioSource.volume = 0.0f; // Đảm bảo không nhỏ hơn 0.0
            }
        }
    }

}
