using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour
{
    private Button button;
    private static Button lastSelectedButton;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }
    private void HandleClick()
    {
        UpDownManager.Instance.nameSideBet = button.name;
        UpDownManager.Instance.footer.SetActive(true);
        if (UpDownManager.Instance.nameSideBet == "BetUnderButton")
        {
            UpDownManager.Instance.moneyBetUnder.text = "0";
            UpDownManager.Instance.moneyBetOver.text = "Bet";
            UpDownManager.Instance.moneyBetUnder.color = Color.red;
        }
        if (UpDownManager.Instance.nameSideBet == "BetOverButton")
        {
            UpDownManager.Instance.moneyBetOver.text = "0";
            UpDownManager.Instance.moneyBetUnder.text = "Bet";
            UpDownManager.Instance.moneyBetOver.color = Color.red;
        }

        if (lastSelectedButton != null && lastSelectedButton != button)
        {
            lastSelectedButton.image.color = Color.yellow;
        }
        button.image.color = Color.black;
        lastSelectedButton = button;
    }
}
