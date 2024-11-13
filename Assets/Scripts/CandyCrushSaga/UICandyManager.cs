using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UICandyManager : MonoBehaviour
{
    public static UICandyManager instance;
    private CandyCrushData screenData;
    private Canvas canvas;

    private GameObject startScreen;
    private GameObject playScreen;
    private GameObject gameOverScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitLize();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadScreenCandyData());
    }

    private void InitLize()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private IEnumerator LoadScreenCandyData()
    {
        AsyncOperationHandle<CandyCrushData> handle = Addressables.LoadAssetAsync<CandyCrushData>("ScreenCandyDataAdress");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            screenData = handle.Result;
            CreateScreen();
        }
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in screenData.datas)
        {
            if (item.screenName == name) return item.screenPrefab;
        }
        Debug.LogWarning("Prefab not found!");
        return null;
    }

    private void CreateScreen()
    {
        startScreen = Instantiate(GetPrefabByName("StartScreen"), canvas.transform);
    }


    public void ChangeStateStartScreen(bool state)
    {
        if (startScreen != null)
        {
            startScreen.SetActive(state);
        }
        else
        {
            Debug.Log("Khong có màn hinh fnayf");
        }
    }
}
