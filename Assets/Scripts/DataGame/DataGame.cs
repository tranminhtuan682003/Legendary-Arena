using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class DataGame : MonoBehaviour
{
    public AssetReference heroAssetReference;

    void Start()
    {
        // Tải hero asset bằng AssetReference
        heroAssetReference.LoadAssetAsync<GameObject>().Completed += OnHeroLoaded;
    }

    private void OnHeroLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject heroPrefab = handle.Result;
            Instantiate(heroPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("Hero loaded and instantiated successfully!");
        }
        else
        {
            Debug.LogError("Failed to load hero asset.");
        }
    }
}
