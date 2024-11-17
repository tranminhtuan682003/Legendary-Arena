using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIKnightManager : MonoBehaviour
{
    public static UIKnightManager instance;
    private KnightDatabase knightDatabase;
    private GameObject startScreen;
    private GameObject playScreen;
    private GameObject gameOverScreen;
    private GameObject settingScreen;
    private GameObject informationScreen;

    private Canvas canvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {

    }

    private void InitLize()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private IEnumerator LoadPipeData()
    {
        AsyncOperationHandle<KnightDatabase> handle = Addressables.LoadAssetAsync<KnightDatabase>("KnightScreenDatabase");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            knightDatabase = handle.Result;
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
        startScreen = Instantiate(GetPrefabByName("StartScreen"), canvas.transform);

        ChangeStateStartScreen(false);
    }

    public void ChangeStateStartScreen(bool state)
    {
        startScreen.SetActive(state);
    }
}
