using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A_O_C_button : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        ResetBet();
        switch (button.name)
        {
            case "OK":
                ApplyBet();
                break;
            case "ALLIN":
                ApplyAllIn();
                break;
            default:
                ApplyCancel();
                break;
        }
        UpDownManager.Instance.totalBetMoney = 0;
    }

    private void ResetBet()
    {
        // Reset lại footer và text của các nút cược
        UpDownManager.Instance.footer.SetActive(false);
        UpDownManager.Instance.moneyBetOver.text = "Bet";
        UpDownManager.Instance.moneyBetUnder.text = "Bet";
    }

    private void ApplyBet()
    {
        if (UpDownManager.Instance.totalAsset > 0)
        {
            int surplusMoney = UpDownManager.Instance.totalAsset - UpDownManager.Instance.resultBetMoney;
            UpDownManager.Instance.totalAsset = surplusMoney;
            UpDownManager.Instance.asset.text = surplusMoney.ToString();
            if (UpDownManager.Instance.nameSideBet == "BetUnderButton")
            {
                UpdateBetValues(UpDownManager.Instance.moneyBetUnder2, UpDownManager.Instance.moneyBetOver2, UpDownManager.Instance.totalBetMoney.ToString());
            }
            else if (UpDownManager.Instance.nameSideBet == "BetOverButton")
            {
                UpdateBetValues(UpDownManager.Instance.moneyBetOver2, UpDownManager.Instance.moneyBetUnder2, UpDownManager.Instance.totalBetMoney.ToString());
            }
        }
        else
        {
            Debug.Log("you haven't enough bet money");
            UpDownManager.Instance.resultBetMoney = 0;
            return;
        }
    }

    private void ApplyAllIn()
    {
        if (UpDownManager.Instance.totalAsset > 0)
        {
            UpDownManager.Instance.resultBetMoney = UpDownManager.Instance.totalAsset;
            UpDownManager.Instance.totalAsset = 0;
            UpDownManager.Instance.asset.text = UpDownManager.Instance.totalAsset.ToString();
            if (UpDownManager.Instance.nameSideBet == "BetUnderButton")
            {
                UpdateBetValues(UpDownManager.Instance.moneyBetUnder2, UpDownManager.Instance.moneyBetOver2, UpDownManager.Instance.resultBetMoney.ToString());
            }
            else if (UpDownManager.Instance.nameSideBet == "BetOverButton")
            {
                UpdateBetValues(UpDownManager.Instance.moneyBetOver2, UpDownManager.Instance.moneyBetUnder2, UpDownManager.Instance.resultBetMoney.ToString());
            }
        }
        else
        {
            Debug.Log("you haven't enough bet money");
            return;
        }
    }
    private void ApplyCancel()
    {
        UpDownManager.Instance.asset.text = UpDownManager.Instance.totalPreviousAsset.ToString();
        if (UpDownManager.Instance.nameSideBet == "BetUnderButton")
        {
            UpdateBetValues(UpDownManager.Instance.moneyBetUnder2, UpDownManager.Instance.moneyBetOver2, "");
        }
        else if (UpDownManager.Instance.nameSideBet == "BetOverButton")
        {
            UpdateBetValues(UpDownManager.Instance.moneyBetOver2, UpDownManager.Instance.moneyBetUnder2, "");
        }
    }


    private void UpdateBetValues(TextMeshProUGUI underBet, TextMeshProUGUI overBet, string underBetValue)
    {
        underBet.text = underBetValue;
        overBet.text = "";
    }
}
