using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadDataScreen : MonoBehaviour
{
    [Inject] private UILoadScreenManager uILoadScreenManager;
    private GameObject nameCompany;
    private GameObject nameGame;
    private GameObject loader;
    private Dictionary<string, GameObject> screens;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        StartCoroutine(NextToStart());
    }

    private void InitiaLize()
    {
        screens = new Dictionary<string, GameObject>();
        FindComponent();
        AddScreensToDictionary();
    }

    private void FindComponent()
    {
        nameCompany = transform.Find("Company")?.gameObject;
        nameGame = transform.Find("NameGame")?.gameObject;
        loader = transform.Find("Load")?.gameObject;
    }

    private void AddScreensToDictionary()
    {
        screens.Add("Company", nameCompany);
        screens.Add("NameGame", nameGame);
        screens.Add("Load", loader);
    }

    private IEnumerator NextToStart()
    {
        ShowScreen("Company");
        yield return new WaitForSeconds(1f);
        ShowScreen("NameGame");
        yield return new WaitForSeconds(1f);
        ShowScreen("Load");
        yield return new WaitForSeconds(2f);
        uILoadScreenManager.ShowScreen("SignIn");
    }

    public void ShowScreen(string screenName)
    {
        foreach (var screen in screens.Values)
        {
            screen.SetActive(false);
        }

        if (screens.ContainsKey(screenName))
        {
            screens[screenName].SetActive(true);
        }
    }
}
