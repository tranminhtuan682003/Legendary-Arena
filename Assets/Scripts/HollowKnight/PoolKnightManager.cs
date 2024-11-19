using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolKnightManager : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void CreatePool(GameObject prefab)
    {
        string poolKey = prefab.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<GameObject>());
        }
    }

    public GameObject GetFromPool(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        string poolKey = prefab.name;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            CreatePool(prefab);
        }
        Queue<GameObject> objectPool = poolDictionary[poolKey];
        foreach (GameObject pooledObject in objectPool)
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.transform.position = position; // Đặt lại vị trí
                pooledObject.transform.rotation = rotation; // Đặt lại góc xoay
                pooledObject.SetActive(true);
                return pooledObject;
            }
        }
        GameObject newObject = Instantiate(prefab, position, rotation, transform);
        newObject.SetActive(true);
        newObject.name = prefab.name;
        objectPool.Enqueue(newObject); // Thêm đối tượng mới vào pool
        return newObject;
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
}
