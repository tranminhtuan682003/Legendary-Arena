using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoneyButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI money;
    private TextMeshProUGUI moneyOverTotal;

    private TextMeshProUGUI moneyUnderTotal;
    void Start()
    {
        button = GetComponent<Button>();
        moneyOverTotal = GameObject.Find("MoneyOverTotal").GetComponent<TextMeshProUGUI>();
        moneyUnderTotal = GameObject.Find("MoneyUnderTotal").GetComponent<TextMeshProUGUI>();
        money = button.GetComponent<TextMeshProUGUI>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        UIManager.Instance.moneyBet = money.text;
        if (UIManager.Instance.nameSideBet == "BetOverButton")
        {
            moneyOverTotal.text = UIManager.Instance.moneyBet;
        }
        if (UIManager.Instance.nameSideBet == "BetUnderButton")
        {
            moneyUnderTotal.text = UIManager.Instance.moneyBet;
        }
    }
}
