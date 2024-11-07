using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsButotn : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        UISBManager.Instance.ResetBetAmountDisplay();
        UISBManager.Instance.StateBetTable(false);
        BetManager.Instance.ResetBetAmount();
        UISBManager.Instance.ChangeStateOverBetButton(false);
        UISBManager.Instance.ChangeStateUnderBetButton(false);
        if (button.name == "ChartButton")
        {
            UISBManager.Instance.ChartState(true);
        }
        else if (button.name == "ExitGameButton")
        {
            SceneManager.LoadScene("Lounge");
        }
        else if (button.name == "SettingButton")
        {
            UISBManager.Instance.SettingState(true);
        }
        else
        {
            UISBManager.Instance.DepositState(true);
        }
    }
}
