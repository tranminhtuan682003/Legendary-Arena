using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSetting : MonoBehaviour
{
    private string nameButton;
    private Button button;
    private Dictionary<string, System.Action> buttonActions;
    private bool isSettingActive = false; // Trạng thái hiện tại của SettingButton

    void Start()
    {
        button = GetComponent<Button>();
        nameButton = button.name;

        // Initialize button actions
        buttonActions = new Dictionary<string, System.Action>
        {
            { "MicButton", () => SoundFlappyManager.Instance.PlayMusicSound(true) },
            { "MuteButton", () => SoundFlappyManager.Instance.PlayMusicSound(false) },
            { "InforButton", () => UIFlappyManager.Instance.ChangeStateInformationScreen(true) },
            { "ExitButton", () => FlappyBirdGameManager.Instance.ExitGame() },
            { "SettingButton", ToggleSettingAnimation }
        };

        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (buttonActions.TryGetValue(nameButton, out var action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning("Button not recognized!");
        }
    }

    private void ToggleSettingAnimation()
    {
        if (isSettingActive)
        {
            UIFlappyManager.Instance.ChangeAnimationSetting("Idle");
        }
        else
        {
            UIFlappyManager.Instance.ChangeAnimationSetting("Run");
        }

        isSettingActive = !isSettingActive;
    }
}
