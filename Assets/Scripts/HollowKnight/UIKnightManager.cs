using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class UIKnightManager : MonoBehaviour
{
    private KnightDatabase knightDatabase;
    private KnightDatabase bulletDatabase;

    //gameplay
    private GameObject map1;
    private GameObject knight;
    private GameObject cam;

    private GameObject startScreen;
    private GameObject playScreen;
    private GameObject gameOverScreen;
    private GameObject settingScreen;
    private GameObject informationScreen;

    private GameObject bulletThrow;

    private Canvas canvas;
    private DiContainer container;
    private PoolKnightManager poolKnightManager;

    [Inject]
    public void Construct(DiContainer container, PoolKnightManager poolKnightManager)
    {
        this.container = container;
        this.poolKnightManager = poolKnightManager;
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
        AsyncOperationHandle<KnightDatabase> handleScreen = Addressables.LoadAssetAsync<KnightDatabase>("KnightScreenDatabase");
        AsyncOperationHandle<KnightDatabase> handleBullet = Addressables.LoadAssetAsync<KnightDatabase>("KnightBulletDatabase");

        yield return handleScreen;
        yield return handleBullet;

        if (handleScreen.Status == AsyncOperationStatus.Succeeded)
        {
            knightDatabase = handleScreen.Result;
            CreateScreen();
        }
        if (handleBullet.Status == AsyncOperationStatus.Succeeded)
        {
            bulletDatabase = handleBullet.Result;
            CreateItem();
        }
    }

    private GameObject GetScreenByName(string name)
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

    private GameObject GetBulletByName(string name)
    {
        foreach (var item in bulletDatabase.data)
        {
            if (item.name == name)
            {
                return item.prefab;
            }
        }
        Debug.Log("none prefabs with name");
        return null;
    }



    private void CreateScreen()
    {
        playScreen = container.InstantiatePrefab(GetScreenByName("PlayScreen"), canvas.transform);
        startScreen = container.InstantiatePrefab(GetScreenByName("StartScreen"), canvas.transform);

        ChangeStatePlayScreen(false);
    }
    public void CreateGamePlay1()
    {
        var mapGame1 = GetScreenByName("Map1");
        map1 = container.InstantiatePrefab(mapGame1);
        var prefabCamera = GetScreenByName("Camera");
        cam = container.InstantiatePrefab(prefabCamera);
        var prefab = GetScreenByName("Knight");
        knight = container.InstantiatePrefab(prefab);
    }

    private void CreateItem()
    {
        GameObject bulletPrefab = GetBulletByName("Throw");
        bulletThrow = bulletPrefab;
        poolKnightManager.CreatePool(bulletThrow);
    }


    public void GetBulletThrow(Transform spawnPoint)
    {
        poolKnightManager.GetFromPool(bulletThrow, spawnPoint.transform.position, spawnPoint.transform.rotation);
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
        return GetScreenByName("StartScreen");
    }
    public GameObject GetKnightPlayScreenPrefab()
    {
        return GetScreenByName("PlayScreen");
    }
}
