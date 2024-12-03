using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UILoungeManager : MonoBehaviour
{
    private LoungeScreenDatabase loungeScreenDatabase;
    private Canvas canvas;
    private GameObject screenParent;
    private GameObject navigation;
    private Dictionary<string, GameObject> screens;
    private DiContainer container;

    [Inject]
    public void Construct(DiContainer container)
    {
        this.container = container;
    }

    private void OnEnable()
    {
        Initialize();
        StartCoroutine(LoadScreenData());
    }

    private void Initialize()
    {
        canvas = FindFirstObjectByType<Canvas>();
        screenParent = canvas.transform.Find("ScreenParrent").gameObject;
        screens = new Dictionary<string, GameObject>();
    }

    private IEnumerator LoadScreenData()
    {
        AsyncOperationHandle<LoungeScreenDatabase> handleScreen = Addressables.LoadAssetAsync<LoungeScreenDatabase>("LoungeScreensAddress");
        yield return handleScreen;

        if (handleScreen.Status == AsyncOperationStatus.Succeeded)
        {
            loungeScreenDatabase = handleScreen.Result;
            ShowScreen("HomeScreen");
            CreateNavigation();
        }
    }

    private GameObject FindScreenPrefab(string name)
    {
        var screenData = loungeScreenDatabase.screens.Find(item => item.name == name);
        return screenData?.screen;
    }

    private GameObject CreateScreen(string nameScreen)
    {
        if (screens.ContainsKey(nameScreen))
        {
            return screens[nameScreen];
        }

        var screenPrefab = FindScreenPrefab(nameScreen);
        if (screenPrefab != null)
        {
            GameObject screen = container.InstantiatePrefab(screenPrefab, screenParent.transform);
            screens.Add(nameScreen, screen);
            return screen;
        }

        return null;
    }

    public void ShowScreen(string nameScreen)
    {
        foreach (var screen in screens.Values)
        {
            screen.SetActive(false);
        }
        GameObject screenToShow = CreateScreen(nameScreen);
        if (screenToShow != null)
        {
            screenToShow.SetActive(true);
        }
    }

    private void CreateNavigation()
    {
        navigation = container.InstantiatePrefab(FindScreenPrefab("Navigation"), canvas.transform);
    }

    public void StateNavigation(bool state)
    {
        navigation.SetActive(state);
    }
}
