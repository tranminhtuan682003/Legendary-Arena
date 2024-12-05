using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class SpawnHeroSystem : MonoBehaviour
{
    private HeroDataBase heroDataBase;
    [Inject] private DiContainer container;
    public void Construct(DiContainer container)
    {
        this.container = container;
    }
    private void Awake()
    {
        StartCoroutine(LoadHero());
    }

    private void OnEnable()
    {

    }

    private IEnumerator LoadHero()
    {
        AsyncOperationHandle<HeroDataBase> handleScreen = Addressables.LoadAssetAsync<HeroDataBase>("HeroAddress");
        yield return handleScreen;

        if (handleScreen.Status == AsyncOperationStatus.Succeeded)
        {
            heroDataBase = handleScreen.Result;
            CreateHeroByName("Valhein");
        }
    }

    private GameObject FindHeroByName(string name)
    {
        var heroData = heroDataBase.heros.Find(item => item.name == name);
        return heroData?.hero;
    }

    public void CreateHeroByName(string name)
    {
        var hero = container.InstantiatePrefab(FindHeroByName("Valhein"));
        hero.transform.position = new Vector3(-32, 0, -30.5f);
        Debug.Log("Da tao");
    }
}
