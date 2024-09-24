using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPool Instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private Dictionary<string, float> nextFireTimeDictionary;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        nextFireTimeDictionary = new Dictionary<string, float>();
        InitPool();
    }

    private void InitPool()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
            nextFireTimeDictionary.Add(pool.tag, 0f); // Initialize the next fire time for each tag
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, float fireRate)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Check the fire rate for the given tag
        if (Time.time < nextFireTimeDictionary[tag])
        {
            return null; // Not ready to fire yet
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        // Update the next fire time for this tag
        nextFireTimeDictionary[tag] = Time.time + 1f / fireRate;

        return objectToSpawn;
    }
}
