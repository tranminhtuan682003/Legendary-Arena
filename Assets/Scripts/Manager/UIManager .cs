using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Singleton Instance (Thread-safe)
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var existingInstance = FindObjectOfType<UIManager>();
                if (existingInstance != null)
                {
                    _instance = existingInstance;
                }
                else
                {
                    var newObj = new GameObject("UIManager");
                    _instance = newObj.AddComponent<UIManager>();
                    DontDestroyOnLoad(newObj);
                }
            }
            return _instance;
        }
    }

    private UIDatabase uIDatabase;
    private const string supplementaryTableAddress = "Assets/Scripts/Manager/NavigationManager/UIDatabase.asset";
    private Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();
    private Canvas canvas;

    private void Awake()
    {
        if (_instance == null) { _instance = this; DontDestroyOnLoad(gameObject); }
        else if (_instance != this) Destroy(gameObject);
    }

    private void Start() => InitUIManager();

    private void InitUIManager()
    {
        canvas = FindObjectOfType<Canvas>();
        Addressables.LoadAssetAsync<UIDatabase>(supplementaryTableAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                uIDatabase = handle.Result;
                foreach (var item in uIDatabase.screens)
                {
                    if (item.screenPrefab != null && !screens.ContainsKey(item.screenName))
                    {
                        var screenInstance = Instantiate(item.screenPrefab, canvas.transform);
                        screens.Add(item.screenName, screenInstance);
                        screenInstance.SetActive(false);
                    }
                }
                ShowScreen("Home");
            }
        };
    }

    public void ShowScreen(string screenName)
    {
        foreach (var item in screens) item.Value.SetActive(false);
        if (screens.ContainsKey(screenName)) screens[screenName].SetActive(true);
        else Debug.LogError($"Không tìm thấy màn hình: {screenName}");
    }
}
