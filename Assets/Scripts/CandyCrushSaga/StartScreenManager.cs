using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    private Button playButton;
    private Button exitButton;
    private Button settingButton;
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
        playButton = transform.Find("PlayButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        settingButton = transform.Find("SettingButton")?.GetComponent<Button>();
    }

    private void ButtonOnclick()
    {
        playButton.onClick.AddListener(HandlePlayButton);
        exitButton.onClick.AddListener(HandleExitButton);
        settingButton.onClick.AddListener(() => HandleSettingButtonClick());
    }

    private void HandlePlayButton()
    {
        SoundCandyManager.Instance.PlayButtonClickSound();
        UICandyManager.instance.ChangeStateStartScreen(false);
        UICandyManager.instance.ChangeStatePlayScreen(true);
    }

    private void HandleExitButton()
    {
        SoundCandyManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("Lounge");
        Debug.Log("exit butotn clicked");
    }

    private void HandleSettingButtonClick()
    {
        SoundCandyManager.Instance.PlayButtonClickSound();
        Debug.Log("Setitng butotn clicked");
    }
}
