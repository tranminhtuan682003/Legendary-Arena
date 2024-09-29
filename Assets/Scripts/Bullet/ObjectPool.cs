using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của ObjectPoolManager
        if (Instance == null)
        {
            Instance = this;
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Tạo hoặc lấy pool, nếu pool chưa có, tạo mới
    public void CreatePool(GameObject prefab, int poolSize)
    {
        string poolKey = prefab.name;

        // Kiểm tra nếu pool đã tồn tại
        if (!poolDictionary.ContainsKey(poolKey))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // Tạo các đối tượng ban đầu cho pool
            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }

            poolDictionary.Add(poolKey, objectPool);
        }
    }

    // Lấy một đối tượng từ pool
    public GameObject GetFromPool(GameObject prefab)
    {
        string poolKey = prefab.name;

        if (poolDictionary.ContainsKey(poolKey))
        {
            GameObject objectToReuse = poolDictionary[poolKey].Count > 0 ? poolDictionary[poolKey].Dequeue() : Instantiate(prefab);
            objectToReuse.SetActive(true);
            return objectToReuse;
        }
        else
        {
            Debug.LogWarning($"No pool for {poolKey}");
            return null;
        }
    }

    // Trả đối tượng lại vào pool
    public void ReturnToPool(GameObject prefab, GameObject objectToReturn)
    {
        string poolKey = prefab.name;

        if (poolDictionary.ContainsKey(poolKey))
        {
            objectToReturn.SetActive(false);
            poolDictionary[poolKey].Enqueue(objectToReturn);
        }
        else
        {
            Debug.LogWarning($"No pool for {poolKey}");
        }
    }
}
