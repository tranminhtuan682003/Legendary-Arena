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
        SoundCandyManager.Instance.PlayButtonClickSound();
        UICandyManager.instance.ChangeStateStartScreen(false);
        UICandyManager.instance.ChangeStatePlayScreen(true);
    }

    private void HandleMenuButton()
    {
        SoundCandyManager.Instance.PlayButtonClickSound();
        Debug.Log("Menu butotn clicked");
    }
}
