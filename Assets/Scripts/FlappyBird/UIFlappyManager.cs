using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIFlappyManager : MonoBehaviour
{
    public static UIFlappyManager Instance;
    private FlappyBirdDatabase pipeData;
    private Canvas canvas;
    //screen
    private GameObject loadScreen;
    private GameObject playScreen;
    private GameObject gameOver;
    private GameObject playAgain;

    //Flappy bird and pipe
    private GameObject flappyBird;
    private GameObject pipe1;
    private GameObject pipe2;
    private GameObject pipe3;
    private GameObject pipe4;
    private GameObject pipe5;
    private GameObject pipe6;
    private GameObject spawnPoint;
    private List<GameObject> pipePrefabs = new List<GameObject>();



    private void OnEnable()
    {
        // Đăng ký sự kiện OnGameOver
        FlappyBirdEventManager.OnGameOver += HandleGameOver;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitLize();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        StartCoroutine(LoadPipeData());
    }

    private void InitLize()
    {
        canvas = FindObjectOfType<Canvas>();
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    private IEnumerator LoadPipeData()
    {
        AsyncOperationHandle<FlappyBirdDatabase> handle = Addressables.LoadAssetAsync<FlappyBirdDatabase>("ScreenFlappyDataAdress");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            pipeData = handle.Result;
            SetPrefabs();
            ChangeStateScreenLoad(true);
        }
        else
        {
            Debug.LogError("Failed to load PipeData from Addressables.");
        }
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in pipeData.data)
        {
            if (item.name == name) return item.prefab;
        }
        Debug.LogWarning("Prefab not found!");
        return null;
    }

    private void SetPrefabs()
    {
        loadScreen = Instantiate(GetPrefabByName("LoadScreen"), canvas.transform);
        playScreen = Instantiate(GetPrefabByName("PlayScreen"), canvas.transform);
        gameOver = Instantiate(GetPrefabByName("GameOver"), canvas.transform);
        playAgain = Instantiate(GetPrefabByName("PlayGameAgain"), canvas.transform);
        // Đặt các màn hình ban đầu là không hoạt động  
        loadScreen.SetActive(false);
        playScreen.SetActive(false);
        gameOver.SetActive(false);
        playAgain.SetActive(false);

        //pipe
        pipe1 = GetPrefabByName("Pipe1");
        pipe2 = GetPrefabByName("Pipe2");
        pipe3 = GetPrefabByName("Pipe3");
        pipe4 = GetPrefabByName("Pipe4");
        pipe5 = GetPrefabByName("Pipe5");
        pipe6 = GetPrefabByName("Pipe6");
        pipePrefabs.Add(pipe1);
        pipePrefabs.Add(pipe2);
        pipePrefabs.Add(pipe3);
        pipePrefabs.Add(pipe4);
        pipePrefabs.Add(pipe5);
        pipePrefabs.Add(pipe6);
        CreatePipe();

    }

    public void CreateFlappyBird()
    {
        flappyBird = Instantiate(GetPrefabByName("FlappyBird"));
        flappyBird.transform.position = new Vector3(0, 3.030238f, 0);
    }

    private void CreatePipe()
    {
        PoolPipeManager.Instance.CreatePool(pipe1);
        PoolPipeManager.Instance.CreatePool(pipe2);
        PoolPipeManager.Instance.CreatePool(pipe3);
        PoolPipeManager.Instance.CreatePool(pipe4);
        PoolPipeManager.Instance.CreatePool(pipe5);
        PoolPipeManager.Instance.CreatePool(pipe6);
    }

    public void GetPipe()
    {
        if (pipePrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, pipePrefabs.Count);
            var randomItem = pipePrefabs[randomIndex];
            PoolPipeManager.Instance.GetFromPool(randomItem, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogWarning("pipePrefabs list is empty : " + pipePrefabs.Count);
        }
    }


    public void ChangeStateScreenLoad(bool state)
    {
        loadScreen.SetActive(state);
    }

    public void ChangeStateScreenPlay(bool state)
    {
        playScreen.SetActive(state);
    }

    public void ChangeStateScreenGameOver(bool state)
    {
        gameOver.SetActive(state);
    }

    public void ChangeStateScreenPlayAgain(bool state)
    {
        playAgain.SetActive(state);
    }


    private void HandleGameOver()
    {
        ChangeStateScreenPlay(false);
        ChangeStateScreenGameOver(true);
    }


    private void OnDisable()
    {
        // Hủy đăng ký sự kiện OnGameOver
        FlappyBirdEventManager.OnGameOver -= HandleGameOver;
    }
}
