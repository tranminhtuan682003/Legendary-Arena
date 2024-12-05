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
        poolSoundKnightManager.CreatePool("MusicPlay", musicPlay, 2);
        poolSoundKnightManager.CreatePool("GetReady", GetSoundByName("GetReady"), 2);
        poolSoundKnightManager.CreatePool("TowerTeamDestroyed", GetSoundByName("TowerTeamDestroyed"), 2);
        poolSoundKnightManager.CreatePool("TowerEnemyDestroyed", GetSoundByName("TowerEnemyDestroyed"), 2);
        poolSoundKnightManager.CreatePool("Victory", GetSoundByName("Victory"), 2);
        poolSoundKnightManager.CreatePool("DefeatEnemy", GetSoundByName("DefeatEnemy"), 2);
        poolSoundKnightManager.CreatePool("youDefeat", GetSoundByName("youDefeat"), 2);
        poolSoundKnightManager.CreatePool("ShutDown", GetSoundByName("ShutDown"), 2);
        poolSoundKnightManager.CreatePool("FirstBlood", GetSoundByName("FirstBlood"), 2);
        poolSoundKnightManager.CreatePool("Legendary", GetSoundByName("Legendary"), 2);
        poolSoundKnightManager.CreatePool("Ultimate", GetSoundByName("Ultimate"), 2);

        //skill
        poolSoundKnightManager.CreatePool("Skill1", GetSoundByName("Skill1"), 10);
        poolSoundKnightManager.CreatePool("Skill2", GetSoundByName("Skill2"), 10);
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

    public void PlayMusicGetReady()
    {
        poolSoundKnightManager.GetFromPool("GetReady", false, 1);
    }

    public void PlayMusicTowerTeamDestroyed()
    {
        poolSoundKnightManager.GetFromPool("TowerTeamDestroyed", false, 1);
    }

    public void PlayMusicTowerEnemyDestroyed()
    {
        poolSoundKnightManager.GetFromPool("TowerEnemyDestroyed", false, 1);
    }

    public void PlayMusicVictory()
    {
        poolSoundKnightManager.GetFromPool("Victory", false, 1);
    }

    public void PlayMusicDefeatEnemy()
    {
        poolSoundKnightManager.GetFromPool("DefeatEnemy", false, 1);
    }

    public void PlayMusicYouDefeat()
    {
        poolSoundKnightManager.GetFromPool("youDefeat", false, 1);
    }

    public void PlayMusicShutDown()
    {
        poolSoundKnightManager.GetFromPool("ShutDown", false, 1);
    }
    public void PlayMusicFirstBlood()
    {
        poolSoundKnightManager.GetFromPool("FirstBlood", false, 1);
    }

    public void PlayMusicLegendary()
    {
        poolSoundKnightManager.GetFromPool("Legendary", false, 1);
    }
    public void PlayMusicUltimate()
    {
        poolSoundKnightManager.GetFromPool("Ultimate", false, 1);
    }

    public void PlayMusicSkill1()
    {
        poolSoundKnightManager.GetFromPool("Skill1", false, 1);
    }

    public void PlayMusicSkill2()
    {
        poolSoundKnightManager.GetFromPool("Skill2", false, 1);
    }


    /// <summary>
    /// Tăng âm lượng lên 10%.
    /// </summary>
    public void IncreaseVolume()
    {
        poolSoundKnightManager.IncreaseVolume(0.1f); // Tăng 10%
    }

    /// <summary>
    /// Giảm âm lượng đi 10%.
    /// </summary>
    public void DecreaseVolume()
    {
        poolSoundKnightManager.DecreaseVolume(0.1f); // Giảm 10%
    }

}
