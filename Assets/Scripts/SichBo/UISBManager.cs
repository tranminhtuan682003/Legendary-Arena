using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UISBManager : MonoBehaviour
{
    public static UISBManager Instance;

    public TextMeshProUGUI idGame;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI balanceText;

    public TextMeshProUGUI fakePlayerOver;
    public TextMeshProUGUI fakePlayerUnder;
    public TextMeshProUGUI fakebetMoneyOver;
    public TextMeshProUGUI fakebetMoneyUnder;

    public TextMeshProUGUI betAmountOverText;
    public TextMeshProUGUI confirmBetAmoutOverText;
    public TextMeshProUGUI betAmountUnderText;
    public TextMeshProUGUI confirmBetAmoutUnderText;

    public TextMeshProUGUI resultText;

    public GameObject betTable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateDateDisplay();
    }

    public void UpdateDateDisplay()
    {
        DateTime currentDate = DateTime.Now;
        int randomSuffix = UnityEngine.Random.Range(100, 1000);

        idGame.text = $"{currentDate:ddMMyyyy}-{randomSuffix}";
    }

    public void UpdatePlayerBalance(int balance)
    {
        balanceText.text = balance.ToString();
    }

    private TextMeshProUGUI GetBetText(BetType betType)
    {
        return betType == BetType.Tai ? betAmountOverText : betAmountUnderText;
    }

    private TextMeshProUGUI GetConfirmBetText(BetType betType)
    {
        return betType == BetType.Tai ? confirmBetAmoutOverText : confirmBetAmoutUnderText;
    }

    public void UpdateBetAmountDisplay(int amount)
    {
        GetBetText(BetManager.Instance.currentBetType).text = amount.ToString();
    }

    public void UpdateConfirmBetAmountDisplay(int amount)
    {
        var confirmBetText = GetConfirmBetText(BetManager.Instance.currentBetType);
        var otherConfirmBetText = confirmBetText == confirmBetAmoutOverText ? confirmBetAmoutUnderText : confirmBetAmoutOverText;
        confirmBetText.text = amount.ToString();
        otherConfirmBetText.text = "";
    }


    public void ResetBetAmountDisplay()
    {
        GetBetText(BetManager.Instance.currentBetType).text = "Bet";
    }

    public void ResetConfirmBetAmountDisplay()
    {
        GetConfirmBetText(BetManager.Instance.currentBetType).text = "";
    }

    public void ShowResult(BetResult result)
    {
        resultText.text = result == BetResult.Win ? "You Win!" : "You Lose!";
    }
}
