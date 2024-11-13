using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameCandyManager : MonoBehaviour
{
    public static GameCandyManager instance;
    private CandyCrushData screenData;
    private CandyCrushData backgroundCandyData;
    //candy1
    private GameObject candyRed1;
    private GameObject candyGreen1;
    private GameObject candyYellow1;
    private GameObject candyBlue1;
    private GameObject candyOrange1;
    private List<GameObject> candy1;
    //backgroundCandy
    private GameObject bgBuble;
    private GameObject bgGrass1;
    private GameObject bgGrass2;
    private GameObject bgGrass3;
    private List<GameObject> backgroundCandys;


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
        StartCoroutine(LoadCandyData());
    }

    private void InitLize()
    {
        candy1 = new List<GameObject>();
        backgroundCandys = new List<GameObject>();
    }

    private IEnumerator LoadCandyData()
    {
        AsyncOperationHandle<CandyCrushData> handleBackground = Addressables.LoadAssetAsync<CandyCrushData>("BackgroundCandyAddress");
        AsyncOperationHandle<CandyCrushData> handleCandy = Addressables.LoadAssetAsync<CandyCrushData>("CandyDatabaseAddress");

        yield return handleBackground;
        yield return handleCandy;

        if (handleCandy.Status == AsyncOperationStatus.Succeeded)
        {
            screenData = handleCandy.Result;  // Lưu dữ liệu của CandyDatabase vào screenData
            CreateCandy();
        }

        if (handleBackground.Status == AsyncOperationStatus.Succeeded)
        {
            backgroundCandyData = handleBackground.Result;
            CreateBackgroundCandy();
        }
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in screenData.datas)
        {
            if (item.screenName == name) return item.screenPrefab;
        }
        Debug.LogWarning("Prefab not found for name: " + name);
        return null;
    }


    private GameObject GetBackgroundbByName(string name)
    {
        foreach (var item in backgroundCandyData.datas)
        {
            if (item.screenName == name) return item.screenPrefab;
        }
        Debug.LogWarning("Prefab not found for name: " + name);
        return null;
    }


    private void CreateCandy()
    {
        // Lấy các prefab và thêm vào danh sách candy1
        candyRed1 = GetPrefabByName("CandyRed1");
        candyGreen1 = GetPrefabByName("CandyGreen1");
        candyBlue1 = GetPrefabByName("CandyBlue1");
        candyYellow1 = GetPrefabByName("CandyYellow1");
        candyOrange1 = GetPrefabByName("CandyOrange1");

        // Gán loại kẹo cho từng prefab
        candyRed1.GetComponent<Candy>().candyType = CandyType.Red;
        candyGreen1.GetComponent<Candy>().candyType = CandyType.Green;
        candyBlue1.GetComponent<Candy>().candyType = CandyType.Blue;
        candyYellow1.GetComponent<Candy>().candyType = CandyType.Yellow;
        candyOrange1.GetComponent<Candy>().candyType = CandyType.Orange;

        // Thêm các viên kẹo vào danh sách candy1
        candy1.Add(candyRed1);
        candy1.Add(candyGreen1);
        candy1.Add(candyBlue1);
        candy1.Add(candyYellow1);
        candy1.Add(candyOrange1);

        // Tạo pool cho từng loại kẹo
        foreach (var item in candy1)
        {
            PoolCandy.instance.CreatePool(item);
        }
    }

    private void CreateBackgroundCandy()
    {
        bgBuble = GetBackgroundbByName("Buble");
        bgGrass1 = GetBackgroundbByName("Grass1");
        bgGrass2 = GetBackgroundbByName("Grass2");
        bgGrass3 = GetBackgroundbByName("Grass3");

        backgroundCandys.Add(bgBuble);
        backgroundCandys.Add(bgGrass1);
        backgroundCandys.Add(bgGrass2);
        backgroundCandys.Add(bgGrass3);

        foreach (var item in backgroundCandys)
        {
            PoolCandy.instance.CreatePool(item);
        }
    }

    public GameObject GetCandy(Vector3 position, Quaternion rotation)
    {
        int randomIndex = Random.Range(0, candy1.Count);
        var randomItem = candy1[randomIndex];
        var candy = PoolCandy.instance.GetFromPool(randomItem, position, rotation);
        return candy;
    }

    public void GetBackgroundBuble(Vector3 position, Quaternion rotation)
    {
        PoolCandy.instance.GetFromPool(bgBuble, position, rotation);
    }
}
