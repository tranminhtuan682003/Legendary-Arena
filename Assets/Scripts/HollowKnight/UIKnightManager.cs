using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class UIKnightManager : MonoBehaviour
{
    private KnightDatabase knightDatabase;
    private GameObject startScreen;
    private GameObject playScreen;
    private GameObject gameOverScreen;
    private GameObject settingScreen;
    private GameObject informationScreen;

    private Canvas canvas;
    private DiContainer container;

    [Inject]
    public void Construct(DiContainer container)
    {
        this.container = container;
    }

    private void Awake()
    {
        InitLize();
        StartCoroutine(LoadScreenData());
    }
    private void Start()
    {
    }

    private void InitLize()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private IEnumerator LoadScreenData()
    {
        AsyncOperationHandle<KnightDatabase> handle = Addressables.LoadAssetAsync<KnightDatabase>("KnightScreenDatabase");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            knightDatabase = handle.Result;
            CreatePrefab();
        }
        else
        {
            Debug.LogError("Failed to load PipeData from Addressables.");
        }
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in knightDatabase.data)
        {
            if (item.name == name)
            {
                return item.prefab;
            }
        }
        Debug.Log("none prefabs with name");
        return null;
    }

    private void CreatePrefab()
    {

        playScreen = container.InstantiatePrefab(GetPrefabByName("PlayScreen"), canvas.transform);
        startScreen = container.InstantiatePrefab(GetPrefabByName("StartScreen"), canvas.transform);

        ChangeStatePlayScreen(false);
    }


    public void ChangeStateStartScreen(bool state)
    {
        if (startScreen == null)
        {
            Debug.Log("Null");
        }
        else
        {
            startScreen.SetActive(state);
        }
    }

    public void ChangeStatePlayScreen(bool state)
    {
        playScreen.SetActive(state);
    }

    public GameObject GetKnightStartScreenPrefab()
    {
        return GetPrefabByName("StartScreen");
    }
    public GameObject GetKnightPlayScreenPrefab()
    {
        return GetPrefabByName("PlayScreen");
    }
}
