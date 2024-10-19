using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMoneyButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        UpDownManager.Instance.moneyBet = button.name;
        int value = int.Parse(UpDownManager.Instance.moneyBet);
        UpDownManager.Instance.totalBetMoney += value;
        UpDownManager.Instance.resultBetMoney = UpDownManager.Instance.totalBetMoney;
        if (UpDownManager.Instance.nameSideBet == "BetUnderButton")
        {
            UpDownManager.Instance.moneyBetUnder.text = UpDownManager.Instance.totalBetMoney.ToString();
        }
        if (UpDownManager.Instance.nameSideBet == "BetOverButton")
        {
            UpDownManager.Instance.moneyBetOver.text = UpDownManager.Instance.totalBetMoney.ToString();
        }
    }
}
