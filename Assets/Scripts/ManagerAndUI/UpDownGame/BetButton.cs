using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour
{
    private Button button;
    private GameObject betLevel;
    void Start()
    {
        button = GetComponent<Button>();
        betLevel = GameObject.Find("BetLevel");
        button.onClick.AddListener(HandleClick);
        if (button.name == "BetUnderButton")
        {
            betLevel.SetActive(false);
        }
    }

    private void HandleClick()
    {
        UIManager.Instance.nameSideBet = button.name;
        UIManager.Instance.isBet = !UIManager.Instance.isBet;
        betLevel.SetActive(UIManager.Instance.isBet);
    }
}
