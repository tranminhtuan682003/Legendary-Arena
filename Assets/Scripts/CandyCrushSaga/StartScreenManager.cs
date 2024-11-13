using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    private Button playButton;
    private Button menuButton;
    private void Awake()
    {
        InitLize();
    }

    void Start()
    {
        ButtonOnclick();
    }

    private void InitLize()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
    }

    private void ButtonOnclick()
    {
        playButton.onClick.AddListener(HandlePlayButton);
        menuButton.onClick.AddListener(HandleMenuButton);
    }

    private void HandlePlayButton()
    {
        UICandyManager.instance.ChangeStateStartScreen(false);
    }

    private void HandleMenuButton()
    {
        Debug.Log("Menu butotn clicked");
    }
}
