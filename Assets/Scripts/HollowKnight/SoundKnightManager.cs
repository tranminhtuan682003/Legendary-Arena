using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class SoundKnightManager : MonoBehaviour
{
    private SoundKnightDatabase soundKnightDatabase;
    private Dictionary<string, AudioClip> soundDictionary;
    private PoolSoundKnightManager poolSoundKnightManager;
    private AudioListener audioListener;

    [Inject]
    public void Construct(PoolSoundKnightManager poolSoundKnightManager)
    {
        this.poolSoundKnightManager = poolSoundKnightManager;
    }

    private void Awake()
    {
        audioListener = gameObject.AddComponent<AudioListener>();
        soundDictionary = new Dictionary<string, AudioClip>();
        StartCoroutine(LoadSound());
    }

    private IEnumerator LoadSound()
    {
        AsyncOperationHandle<SoundKnightDatabase> handleSound = Addressables.LoadAssetAsync<SoundKnightDatabase>("SoundKnightDataAddress");
        yield return handleSound;

        if (handleSound.Status == AsyncOperationStatus.Succeeded)
        {
            soundKnightDatabase = handleSound.Result;

            foreach (var item in soundKnightDatabase.datas)
            {
                soundDictionary[item.name] = item.audio;
            }
            CreatePoolSounds();
            PlayMusicStart();
        }
    }

    private void CreatePoolSounds()
    {
        var musicStart = GetSoundByName("MusicStart");
        var musicPlay = GetSoundByName("MusicPlay");

        poolSoundKnightManager.CreatePool("MusicStart", musicStart, 2);
        // poolSoundKnightManager.CreatePool("MusicPlay", musicPlay, 2);
    }


    private AudioClip GetSoundByName(string nameSound)
    {
        if (soundDictionary.TryGetValue(nameSound, out var clip))
        {
            return clip;
        }
        return null;
    }

    public void PlayMusicStart()
    {
        poolSoundKnightManager.GetFromPool("MusicStart", true, 1);
    }

    public void PlayMusicPlay()
    {
        poolSoundKnightManager.GetFromPool("MusicPlay", true, 1);
    }
}
