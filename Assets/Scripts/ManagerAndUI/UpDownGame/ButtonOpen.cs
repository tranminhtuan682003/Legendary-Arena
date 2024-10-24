using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOpen : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }
    private void HandleClick()
    {
        if (button.name == "OpenChart")
        {
            UpDownManager.Instance.chart.SetActive(true);
        }
        else if (button.name == "ExitChart")
        {
            UpDownManager.Instance.chart.SetActive(false);
        }
        else if (button.name == "OpenInformation")
        {
            Debug.Log("Open Information Panel");
        }
        else if (button.name == "ExitInformation")
        {
            Debug.Log("Exit Information Panel");
        }
    }
}
