using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISBManager : MonoBehaviour
{
    public static UISBManager Instance;

    public TextMeshProUGUI idGame;
    private TextMeshProUGUI countdownText;
    private Animator animator;
    private TextMeshProUGUI balanceText;

    private TextMeshProUGUI fakePlayerOver;
    private TextMeshProUGUI fakePlayerUnder;
    private TextMeshProUGUI fakebetMoneyOver;
    private TextMeshProUGUI fakebetMoneyUnder;

    private TextMeshProUGUI betAmountOverText;
    private TextMeshProUGUI confirmBetAmoutOverText;
    private TextMeshProUGUI betAmountUnderText;
    private TextMeshProUGUI confirmBetAmoutUnderText;

    private Button overBetButton;
    private Button underBetButton;

    private TextMeshProUGUI resultText;
    private GameObject betTable;

    private GameObject chart;

    private List<int> generatedIds = new List<int>(); // Danh sách lưu các ID đã tạo
    private const string IdFileName = "GeneratedIds.json"; // Tên file JSON

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeUIElements();
        StateBetTable(false);
        ChartState(false);
        LoadGeneratedIds();
    }

    private void Start()
    {
        UpdateDateDisplay();

        if (countdownText != null)
        {
            animator = countdownText.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("CountdownText not found. Animator not initialized.");
        }
    }

    #region UI Initialization
    private void InitializeUIElements()
    {
        idGame = GameObject.Find("IdGameText")?.GetComponent<TextMeshProUGUI>();
        countdownText = GameObject.Find("CountdownText")?.GetComponent<TextMeshProUGUI>();
        balanceText = GameObject.Find("BalanceText")?.GetComponent<TextMeshProUGUI>();

        fakePlayerOver = GameObject.Find("FakePlayerOver")?.GetComponent<TextMeshProUGUI>();
        fakePlayerUnder = GameObject.Find("FakePlayerUnder")?.GetComponent<TextMeshProUGUI>();
        fakebetMoneyOver = GameObject.Find("FakeBetMoneyOver")?.GetComponent<TextMeshProUGUI>();
        fakebetMoneyUnder = GameObject.Find("FakeBetMoneyUnder")?.GetComponent<TextMeshProUGUI>();

        betAmountOverText = GameObject.Find("BetAmountOverText")?.GetComponent<TextMeshProUGUI>();
        confirmBetAmoutOverText = GameObject.Find("ConfirmBetAmountOverText")?.GetComponent<TextMeshProUGUI>();
        betAmountUnderText = GameObject.Find("BetAmountUnderText")?.GetComponent<TextMeshProUGUI>();
        confirmBetAmoutUnderText = GameObject.Find("ConfirmBetAmountUnderText")?.GetComponent<TextMeshProUGUI>();

        resultText = GameObject.Find("ResultText")?.GetComponent<TextMeshProUGUI>();
        betTable = GameObject.Find("BetTable");

        overBetButton = GameObject.Find("OverBetButton")?.GetComponent<Button>();
        underBetButton = GameObject.Find("UnderBetButton")?.GetComponent<Button>();

        chart = GameObject.Find("Chart");
    }
    #endregion

    #region Animation
    public void ChangeAnimationTimer(string nameAnimation)
    {
        if (animator != null)
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("ShowResult");
            animator.SetTrigger(nameAnimation);
        }
        else
        {
            Debug.LogError("Animator is null. ChangeAnimationTimer cannot set animation.");
        }
    }
    #endregion

    #region UI Updates
    public void UpdateDateDisplay()
    {
        DateTime currentDate = DateTime.Now;
        int newId = generatedIds.Count > 0 ? generatedIds[generatedIds.Count - 1] + 1 : 100;
        generatedIds.Add(newId);

        if (idGame != null)
        {
            idGame.text = $"{currentDate:ddMMyyyy}-{newId}";
        }
        SaveGeneratedIds();
    }

    public void UpdateCountdownText(int text)
    {
        if (countdownText != null)
        {
            countdownText.text = text == 0 ? "" : text.ToString();
        }
    }

    public void StateBetTable(bool active)
    {
        if (betTable != null)
        {
            betTable.SetActive(active);
        }
    }

    public void UpdateFakePlayerAndBet(int fakePlayersOver, int fakePlayersUnder, int fakeBetMoneyOver, int fakeBetMoneyUnder)
    {
        if (fakePlayerOver != null) fakePlayerOver.text = fakePlayersOver.ToString();
        if (fakePlayerUnder != null) fakePlayerUnder.text = fakePlayersUnder.ToString();
        if (fakebetMoneyOver != null) fakebetMoneyOver.text = fakeBetMoneyOver.ToString();
        if (fakebetMoneyUnder != null) fakebetMoneyUnder.text = fakeBetMoneyUnder.ToString();
    }

    public void UpdatePlayerBalance(int balance)
    {
        if (balanceText != null)
        {
            balanceText.text = balance.ToString();
        }
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
        var betText = GetBetText(BetManager.Instance.currentBetType);
        if (betText != null)
        {
            betText.text = amount.ToString();
        }
    }

    public void UpdateConfirmBetAmountDisplay(int amount)
    {
        var confirmBetText = GetConfirmBetText(BetManager.Instance.currentBetType);
        var otherConfirmBetText = confirmBetText == confirmBetAmoutOverText ? confirmBetAmoutUnderText : confirmBetAmoutOverText;

        if (confirmBetText != null) confirmBetText.text = amount.ToString();
        if (otherConfirmBetText != null) otherConfirmBetText.text = "";
    }

    public void ResetBetAmountDisplay()
    {
        var betText = GetBetText(BetManager.Instance.currentBetType);
        if (betText != null)
        {
            betText.text = "Bet";
        }
    }

    public void ResetConfirmBetAmountDisplay()
    {
        var confirmBetText = GetConfirmBetText(BetManager.Instance.currentBetType);
        if (confirmBetText != null)
        {
            confirmBetText.text = "";
        }
    }

    public void RefeshUI()
    {
        if (confirmBetAmoutOverText != null) confirmBetAmoutOverText.text = "";
        if (confirmBetAmoutUnderText != null) confirmBetAmoutUnderText.text = "";
    }

    public void ShowResult(BetResult result)
    {
        if (resultText != null)
        {
            resultText.text = result == BetResult.Win ? "You Win!" : "You Lose!";
        }
    }
    #endregion

    #region Button States
    public void ChangeStateOverBetButton(bool state)
    {
        if (overBetButton != null && betAmountOverText != null)
        {
            overBetButton.image.color = state ? Color.black : Color.yellow;
            betAmountOverText.color = state ? Color.yellow : Color.black;
        }
    }

    public void ChangeStateUnderBetButton(bool state)
    {
        if (underBetButton != null && betAmountUnderText != null)
        {
            underBetButton.image.color = state ? Color.black : Color.yellow;
            betAmountUnderText.color = state ? Color.yellow : Color.black;
        }
    }

    public void ActiveOverButton(bool active)
    {
        if (overBetButton != null)
        {
            overBetButton.gameObject.SetActive(active);
        }
    }

    public void ActiveUnderButton(bool active)
    {
        if (underBetButton != null)
        {
            underBetButton.gameObject.SetActive(active);
        }
    }
    #endregion

    #region Options manage
    public void ChartState(bool state)
    {
        if (chart != null)
        {
            chart.SetActive(state);

            if (state)
            {
                ScrollRect scrollView = chart.GetComponentInChildren<ScrollRect>();
                if (scrollView != null)
                {
                    scrollView.verticalNormalizedPosition = 1f;
                }
            }
        }
    }


    public void SettingState(bool state)
    {
        // if (chart != null)
        // {
        //     chart.SetActive(state);
        // }
        Debug.Log("Setiing");
    }

    public void DepositState(bool state)
    {
        // if (chart != null)
        // {
        //     chart.SetActive(state);
        // }
        Debug.Log("Deposit");
    }



    #endregion

    #region ID Persistence
    private void SaveGeneratedIds()
    {
        bool success = JsonFileHandler.SaveToFile(generatedIds, IdFileName);
        if (!success)
        {
            Debug.LogError("Failed to save generated IDs.");
        }
    }

    private void LoadGeneratedIds()
    {
        generatedIds = JsonFileHandler.LoadFromFile<List<int>>(IdFileName) ?? new List<int>();
    }
    #endregion
}
