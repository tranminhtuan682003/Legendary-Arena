using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemyKnightManager : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> poolDictionary; // Quản lý pool theo tên

    private void Awake()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();
    }

    public void CreatePool(string poolName, GameObject prefab, int initialSize)
    {
        if (string.IsNullOrEmpty(poolName) || prefab == null)
        {
            Debug.LogError("Invalid pool name or prefab. Cannot create pool.");
            return;
        }

        if (!poolDictionary.ContainsKey(poolName))
        {
            poolDictionary[poolName] = new List<GameObject>();
        }

        List<GameObject> objectPool = poolDictionary[poolName];

        for (int i = 0; i < initialSize; i++)
        {
            GameObject newObject = InstantiateNewObject(prefab);
            objectPool.Add(newObject);
        }
    }

    public GameObject GetFromPool(string poolName, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool '{poolName}' does not exist. Make sure to create the pool first.");
            return null;
        }

        List<GameObject> objectPool = poolDictionary[poolName];

        // Tìm đối tượng không hoạt động
        foreach (GameObject pooledObject in objectPool)
        {
            if (!pooledObject.activeInHierarchy)
            {
                ActivateObject(pooledObject, position, rotation);
                return pooledObject;
            }
        }

        Debug.LogWarning($"No inactive objects in pool '{poolName}'. Consider increasing its size.");
        return null;
    }

    public void DeactivateAllObjects(string poolName)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool '{poolName}' does not exist.");
            return;
        }

        List<GameObject> objectPool = poolDictionary[poolName];

        foreach (GameObject pooledObject in objectPool)
        {
            if (pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(false);
            }
        }
    }


    public void DeactivateAllObjects()
    {
        foreach (var pool in poolDictionary)
        {
            foreach (var pooledObject in pool.Value)
            {
                if (pooledObject.activeInHierarchy)
                {
                    pooledObject.SetActive(false);
                }
            }
        }
    }

    private GameObject InstantiateNewObject(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform);
        newObject.name = prefab.name; // Đồng bộ tên với prefab để dễ quản lý
        newObject.SetActive(false);
        return newObject;
    }

    private void ActivateObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
    }
}
