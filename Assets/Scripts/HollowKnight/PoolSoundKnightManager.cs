using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSoundKnightManager : MonoBehaviour
{
    private Dictionary<string, List<AudioSource>> poolDictionary; // Quản lý pool theo tên
    private Dictionary<string, AudioClip> clipDictionary;         // Ánh xạ tên pool với AudioClip

    [Header("AudioSource Settings")]
    public float defaultVolume = 1.0f; // Âm lượng mặc định
    public bool loopByDefault = false; // Vòng lặp mặc định

    private void Awake()
    {
        poolDictionary = new Dictionary<string, List<AudioSource>>();
        clipDictionary = new Dictionary<string, AudioClip>();
    }

    /// <summary>
    /// Tạo một pool với tên, AudioClip và số lượng ban đầu.
    /// </summary>
    public void CreatePool(string poolName, AudioClip clip, int initialSize)
    {
        if (string.IsNullOrEmpty(poolName) || clip == null)
        {
            Debug.LogError("Invalid pool name or AudioClip. Cannot create pool.");
            return;
        }

        if (!poolDictionary.ContainsKey(poolName))
        {
            poolDictionary[poolName] = new List<AudioSource>();
            clipDictionary[poolName] = clip; // Liên kết AudioClip với poolName
        }

        List<AudioSource> audioList = poolDictionary[poolName];

        for (int i = 0; i < initialSize; i++)
        {
            AudioSource newAudioSource = CreateNewAudioSource(poolName, clip);
            audioList.Add(newAudioSource);
        }
    }

    /// <summary>
    /// Lấy một AudioSource từ pool.
    /// </summary>
    public AudioSource GetFromPool(string poolName, bool loop = false, float volume = -1f)
    {
        if (!poolDictionary.ContainsKey(poolName) || !clipDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool '{poolName}' does not exist. Make sure to create the pool first.");
            return null;
        }

        List<AudioSource> audioList = poolDictionary[poolName];
        AudioClip clip = clipDictionary[poolName];

        // Tìm một AudioSource không hoạt động
        foreach (AudioSource audioSource in audioList)
        {
            if (!audioSource.gameObject.activeInHierarchy)
            {
                ActivateAudioSource(audioSource, clip, loop, volume);
                return audioSource;
            }
        }

        // Nếu không tìm thấy, tạo mới AudioSource
        AudioSource newAudioSource = CreateNewAudioSource(poolName, clip);
        audioList.Add(newAudioSource);
        ActivateAudioSource(newAudioSource, clip, loop, volume);
        return newAudioSource;
    }

    /// <summary>
    /// Tự động trả AudioSource về pool sau khi phát xong.
    /// </summary>
    private IEnumerator ReturnToPoolWhenFinished(AudioSource audioSource)
    {
        audioSource.Play();
        yield return new WaitUntil(() => !audioSource.isPlaying && !audioSource.loop);
        ReturnToPool(audioSource);
    }

    /// <summary>
    /// Trả AudioSource về pool và tắt nó.
    /// </summary>
    private void ReturnToPool(AudioSource audioSource)
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogWarning("AudioSource is null or clip is missing. Skipping return to pool.");
            return;
        }

        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
    }

    /// <summary>
    /// Tạo mới một AudioSource.
    /// </summary>
    private AudioSource CreateNewAudioSource(string poolName, AudioClip clip)
    {
        GameObject audioObject = new GameObject($"AudioSource_{poolName}");
        audioObject.transform.parent = transform;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        audioSource.loop = loopByDefault;

        audioObject.SetActive(false); // Đối tượng bắt đầu ở trạng thái không hoạt động
        return audioSource;
    }

    /// <summary>
    /// Kích hoạt AudioSource với các thiết lập cụ thể.
    /// </summary>
    private void ActivateAudioSource(AudioSource audioSource, AudioClip clip, bool loop, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume >= 0 ? volume : defaultVolume;
        audioSource.loop = loop;
        audioSource.gameObject.SetActive(true);

        // Tự động trả về pool sau khi phát xong
        StartCoroutine(ReturnToPoolWhenFinished(audioSource));
    }

    /// <summary>
    /// Tắt tất cả AudioSource trong pool.
    /// </summary>
    public void DeactivateAllAudioSources(string poolName)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool '{poolName}' does not exist.");
            return;
        }

        List<AudioSource> audioList = poolDictionary[poolName];

        foreach (AudioSource audioSource in audioList)
        {
            if (audioSource.gameObject.activeInHierarchy)
            {
                audioSource.Stop();
                audioSource.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Tắt tất cả AudioSource trong tất cả pool.
    /// </summary>
    public void DeactivateAllAudioSources()
    {
        foreach (var pool in poolDictionary)
        {
            foreach (var audioSource in pool.Value)
            {
                if (audioSource.gameObject.activeInHierarchy)
                {
                    audioSource.Stop();
                    audioSource.gameObject.SetActive(false);
                }
            }
        }
    }
}
