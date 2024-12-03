using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UILoadScreenManager : MonoBehaviour
{
    private LoadGameScreens loadGameScreens;
    private DiContainer container;
    private Canvas canvas;
    private Dictionary<string, GameObject> screens;

    // Injecting DiContainer into the class
    [Inject]
    public void Constract(DiContainer container)
    {
        this.container = container;
    }

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        Debug.Log("Hello");
        StartCoroutine(LoadScreen());
    }

    private void InitiaLize()
    {
        // Finding the first Canvas in the scene
        canvas = FindFirstObjectByType<Canvas>();
        screens = new Dictionary<string, GameObject>();
    }

    private IEnumerator LoadScreen()
    {
        // Loading screens asynchronously from Addressables
        AsyncOperationHandle<LoadGameScreens> handleScreen = Addressables.LoadAssetAsync<LoadGameScreens>("ScreensAddress");
        yield return handleScreen;

        if (handleScreen.Status == AsyncOperationStatus.Succeeded)
        {
            loadGameScreens = handleScreen.Result;
            // Show the default screen when the screens are loaded
            ShowScreen("LoadScreen");
        }
        else
        {
            Debug.LogError("Failed to load screens from Addressables.");
        }
    }

    // Method to find the prefab of the screen by name
    private GameObject FindScreenPrefab(string name)
    {
        var screenData = loadGameScreens.screens.Find(item => item.name == name);
        return screenData?.screen;
    }

    // Method to create a screen if it doesn't exist already
    private GameObject CreateScreen(string nameScreen)
    {
        // If the screen already exists, return it
        if (screens.ContainsKey(nameScreen))
        {
            return screens[nameScreen];
        }

        var screenPrefab = FindScreenPrefab(nameScreen);
        if (screenPrefab != null)
        {
            // Instantiate the screen using Zenject and add it to the canvas
            GameObject screen = container.InstantiatePrefab(screenPrefab, canvas.transform);
            screens.Add(nameScreen, screen);
            return screen;
        }

        return null;
    }

    // Method to show a specific screen
    public void ShowScreen(string nameScreen)
    {
        // Hide all existing screens
        foreach (var screen in screens.Values)
        {
            screen.SetActive(false);
        }

        // Create and show the requested screen
        GameObject screenToShow = CreateScreen(nameScreen);
        if (screenToShow != null)
        {
            screenToShow.SetActive(true);
        }
    }

    // Method to show an error message
    public void ShowError(string errorMessage)
    {
        // You can extend this method to show the error message on a UI element
        Debug.LogError(errorMessage);
        // Optionally, show a UI element with the error message
    }
}
