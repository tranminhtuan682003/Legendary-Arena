using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FlappyBirdManager : MonoBehaviour
{
    private FlappyBirdDatabase pipeData;

    void Start()
    {
        StartCoroutine(LoadPipeData());
    }

    private IEnumerator LoadPipeData()
    {
        AsyncOperationHandle<FlappyBirdDatabase> handle = Addressables.LoadAssetAsync<FlappyBirdDatabase>("PipeDataAddress");
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            pipeData = handle.Result;
            Initlize();
        }
        else
        {
            Debug.LogError("Failed to load PipeData from Addressables.");
        }
    }

    private void Initlize()
    {
        foreach (var item in pipeData.data)
        {
            PoolPipeManager.Instance.CreatePool(item.prefab);
        }
    }

    void Update()
    {

    }



}
