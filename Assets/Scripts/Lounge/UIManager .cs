using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                var existingInstance = FindObjectOfType<UIManager>();
                if (existingInstance != null)
                {
                    instance = existingInstance;
                }
                else
                {
                    var newObj = new GameObject("UIManager");
                    instance = newObj.AddComponent<UIManager>();
                    DontDestroyOnLoad(newObj);
                }
            }
            return instance;
        }
    }
    private StartPosition startPosition;
    private UIDatabase uIDatabase;
    private HeroDatabase heroDatabase;
    private SupplementaryDatabase supplementaryDatabase;
    private const string UIDatabaseAddress = "Assets/Scripts/Manager/NavigationManager/UIDatabase.asset";
    private const string HeroDatabaseAddress = "Assets/Scripts/ManagerAndUI/Hero/HeroDatabase.asset";
    private const string SupplementaryDatabaseAddress = "Assets/Scripts/Skill/ScriptTableObject/SupplementaryTable.asset";
    private Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> navigations = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> heros = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> supplymentarys = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> heroBackgrounds = new Dictionary<string, GameObject>();
    private Canvas canvas;
    private RectTransform selectScreen;
    [HideInInspector] public bool isAnyButtonProcessing = false;
    [HideInInspector] public string nameHero;
    [HideInInspector] public bool isDragCamera;
    [HideInInspector] public bool isPickHeroScreen;
    [HideInInspector] public bool isPickHero;
    //Supplymentary
    [HideInInspector] public string extraSkillName;
    [HideInInspector] public bool isPickSupplymentary;

    // SelectGame
    [HideInInspector] public string nameGame;

    public void InitializeBattle()
    {
        Debug.Log("creted");
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitScreen();
    }

    public void ResetBool()
    {
        isPickHeroScreen = false;
        isPickHero = false;
    }

    private void InitScreen()
    {
        canvas = FindObjectOfType<Canvas>();
        Addressables.LoadAssetAsync<UIDatabase>(UIDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                uIDatabase = handle.Result;
                CreateScreen(() =>
                {
                    CreateNavigation();
                    ShowNavigation("Navigation");
                    ShowScreen("Home");
                });
            }
        };
    }

    private void CreateScreen(Action onComplete)
    {
        foreach (var item in uIDatabase.screens)
        {
            if (item.screenPrefab != null && !screens.ContainsKey(item.screenName))
            {
                var screenInstance = Instantiate(item.screenPrefab, canvas.transform);
                screens.Add(item.screenName, screenInstance);
                screenInstance.SetActive(false);
            }
        }
        onComplete?.Invoke(); // Gọi callback sau khi tất cả screen đã được tạo
    }


    public void InitHero()
    {
        Addressables.LoadAssetAsync<HeroDatabase>(HeroDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                heroDatabase = handle.Result;
                CreateHero();
            }
        };
    }

    private void CreateHero()
    {
        if (!heros.ContainsKey(nameHero))
        {
            foreach (var item in heroDatabase.heros)
            {
                if (nameHero == item.heroName)
                {
                    var heroInstance = Instantiate(item.heroPrefab);
                    var heroBase = heroInstance.GetComponent<HeroBase>(); // Lấy HeroBase từ heroInstance

                    if (heroBase.GetTeam() == Team.Blue) // Sử dụng heroBase để gọi getTeam()
                    {
                        heroInstance.transform.position = new Vector3(-32, 0, -30.5f);
                    }
                    else
                    {
                        heroInstance.transform.position = new Vector3(26.45f, 0f, 27.95f);
                        heroInstance.transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
                    }

                    heros.Add(item.heroName, heroInstance);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Khong co tuong nay");
        }
    }


    private void CreateNavigation()
    {
        var nav = Instantiate(uIDatabase.navigations.screenPrefab, canvas.transform);
        navigations.Add(uIDatabase.navigations.screenName, nav);
        selectScreen = GameObject.Find("SelectScreen").GetComponent<RectTransform>();
        nav.SetActive(false);
    }

    public void InitSupplymentary(Transform parent, string nameSup, Action onComplete)
    {
        Addressables.LoadAssetAsync<SupplementaryDatabase>(SupplementaryDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                supplementaryDatabase = handle.Result;
                foreach (var item in supplementaryDatabase.supplymentarys)
                {
                    if (item.nameSup == nameSup)
                    {
                        var sup = Instantiate(item.supPrefab, parent);
                        supplymentarys.Add(item.nameSup, sup);
                        sup.SetActive(false);
                    }
                }

                // Gọi callback khi hoàn tất khởi tạo
                onComplete?.Invoke();
            }
            else
            {
                Debug.LogError("Không thể tải SupplementaryDatabase.");
                onComplete?.Invoke(); // Gọi callback dù có lỗi để tránh treo chương trình
            }
        };
    }


    public GameObject GetSup(string nameSup)
    {
        if (supplymentarys.ContainsKey(nameSup))
        {
            return supplymentarys[nameSup];  // Trả về đối tượng nếu tìm thấy
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy supplymentary có tên: {nameSup}");
            return null;  // Trả về null nếu không tìm thấy
        }
    }

    public void ShowHeroBackground(string nameHero)
    {
        if (heroBackgrounds.ContainsKey(nameHero))
        {
            foreach (var item in heroBackgrounds)
                item.Value.SetActive(false);
            heroBackgrounds[nameHero].SetActive(true);
        }
    }

    public void GetNameHero(string nameHero)
    {
        this.nameHero = nameHero;
    }

    public void ShowScreen(string screenName)
    {
        foreach (var item in screens)
            item.Value.SetActive(false);

        if (screens.ContainsKey(screenName))
            screens[screenName].SetActive(true);
    }

    public void ShowNavigation(string nameNav)
    {
        if (navigations.ContainsKey(nameNav))
            navigations[nameNav].SetActive(true);
    }
    public void HideNavigation(string nameNav)
    {
        if (navigations.ContainsKey(nameNav))
            navigations[nameNav].SetActive(false);
    }

    public void MoveSelectScreen(float moveDuration, RectTransform target)
    {
        StartCoroutine(MoveToTarget(moveDuration, target));
    }

    private IEnumerator MoveToTarget(float moveDuration, RectTransform target)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = selectScreen.position;

        while (elapsedTime < moveDuration)
        {
            selectScreen.position = Vector3.Lerp(startingPos, target.position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        selectScreen.position = target.position;
    }
}
