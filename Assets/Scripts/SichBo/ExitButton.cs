using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (button.name == "ExitChartButton")
        {
            UISBManager.Instance.ChartState(false);
        }
        else if (button.name == "ExitSettingButton")
        {
            UISBManager.Instance.SettingState(false);
        }
        else
        {
            UISBManager.Instance.DepositState(false);
        }
    }
}
