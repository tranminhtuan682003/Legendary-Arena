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
    private GameObject deposit;
    private GameObject setting;
    private GameObject bowl;


    //Dice
    private GameObject dice;
    private Image dice1;
    private Image dice2;
    private Image dice3;

    //Name Over under
    private TextMeshProUGUI nameOver;
    private TextMeshProUGUI nameUnder;

    //SceneTransition
    private GameObject sceneTransition;

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
        DepositState(false);
        SettingState(false);
        SceneTransitionState(false);
        // LoadGeneratedIds();
    }

    private void Start()
    {
        // UpdateDateDisplay();

        // if (countdownText != null)
        // {
        //     animator = countdownText.GetComponent<Animator>();
        // }
        // else
        // {
        //     Debug.LogError("CountdownText not found. Animator not initialized.");
        // }
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
        deposit = GameObject.Find("Deposit");
        setting = GameObject.Find("Setting");

        bowl = GameObject.Find("Bowl");

        dice1 = GameObject.Find("Dice1").GetComponent<Image>();
        dice2 = GameObject.Find("Dice2").GetComponent<Image>();
        dice3 = GameObject.Find("Dice3").GetComponent<Image>();
        dice = GameObject.Find("Dice");

        nameOver = GameObject.Find("NameOver").GetComponent<TextMeshProUGUI>();
        nameUnder = GameObject.Find("NameUnder").GetComponent<TextMeshProUGUI>();

        sceneTransition = GameObject.Find("SceneTransition");

    }
    #endregion

    #region Animation
    public void ChangeAnimationTimer(bool state, string nameAnimationRun, string nameAnimationIdle)
    {
        Animator animator = countdownText.GetComponent<Animator>();
        string animationToTrigger = state ? nameAnimationRun : nameAnimationIdle;
        animator.SetTrigger(animationToTrigger);
    }

    public void ChanAnimationFakeUI(bool state, string nameAnimationRun, string nameAnimationIdle)
    {
        string animationToTrigger = state ? nameAnimationRun : nameAnimationIdle;

        Animator[] animator = {
        fakePlayerOver.GetComponent<Animator>(),
        fakePlayerUnder.GetComponent<Animator>(),
        fakebetMoneyOver.GetComponent<Animator>(),
        fakebetMoneyUnder.GetComponent<Animator>()
    };

        foreach (var item in animator)
        {
            item.SetTrigger(animationToTrigger);
        }
    }

    public void ChangeAnimationBowl(bool state, string nameAnimationRun, string nameAnimationIdle, bool active)
    {
        Animator animator = bowl.GetComponent<Animator>();
        if (active)
        {
            animator.enabled = true;
            string animationToTrigger = state ? nameAnimationRun : nameAnimationIdle;
            animator.SetTrigger(animationToTrigger);
        }
        else
        {
            animator.enabled = false;
        }
    }

    public void ChangeAnimationDice(bool state, string nameAnimationRun, string nameAnimationIdle)
    {
        Animator animator = dice.GetComponent<Animator>();
        string animationToTrigger = state ? nameAnimationRun : nameAnimationIdle;
        animator.SetTrigger(animationToTrigger);
    }

    public void ChangeAnimationNameResult(bool state, bool over)
    {
        Animator animator = over ? nameOver.GetComponent<Animator>() : nameUnder.GetComponent<Animator>();
        string animationToTrigger = state ? "Run" : "Idle";
        animator.SetTrigger(animationToTrigger);
    }

    #endregion

    #region UI Updates
    // public void UpdateDateDisplay()
    // {
    //     DateTime currentDate = DateTime.Now;
    //     int newId = generatedIds.Count > 0 ? generatedIds[generatedIds.Count - 1] + 1 : 100;
    //     generatedIds.Add(newId);

    //     if (idGame != null)
    //     {
    //         idGame.text = $"{currentDate:ddMMyyyy}-{newId}";
    //     }
    //     SaveGeneratedIds();
    // }

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

    public void ResetResultText()
    {
        resultText.text = "";
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

    public void SetImageDice(int dice1, int dice2, int dice3)
    {
        string path = "UISB/Dice/";
        this.dice1.sprite = Resources.Load<Sprite>($"{path}{dice1}");
        this.dice2.sprite = Resources.Load<Sprite>($"{path}{dice2}");
        this.dice3.sprite = Resources.Load<Sprite>($"{path}{dice3}");
    }

    #endregion

    #region Button States
    public void ChangeStateOverBetButton(bool state)
    {
        if (overBetButton != null)
        {
            string spritePath = state ? "UISB/ButtonBetDeactive" : "UISB/ButtonBetActive";
            overBetButton.image.sprite = Resources.Load<Sprite>(spritePath);
            betAmountOverText.color = state ? Color.yellow : Color.black;
        }
    }

    public void ChangeStateUnderBetButton(bool state)
    {
        if (underBetButton != null)
        {
            string spritePath = state ? "UISB/ButtonBetDeactive" : "UISB/ButtonBetActive";
            underBetButton.image.sprite = Resources.Load<Sprite>(spritePath);
            betAmountUnderText.color = state ? Color.yellow : Color.black;
        }
    }


    public void ActiveOverButton(bool active)
    {
        if (overBetButton != null)
        {
            overBetButton.interactable = active;
        }
    }

    public void ActiveUnderButton(bool active)
    {
        if (underBetButton != null)
        {
            underBetButton.interactable = active;
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
        if (setting != null)
        {
            setting.SetActive(state);
        }
    }

    public void DepositState(bool state)
    {
        if (deposit != null)
        {
            deposit.SetActive(state);
        }
    }

    public void SceneTransitionState(bool state)
    {
        if (sceneTransition != null)
        {
            sceneTransition.SetActive(state);
        }
    }



    #endregion

    // #region ID Persistence
    // private void SaveGeneratedIds()
    // {
    //     bool success = JsonFileHandler.SaveToFile(generatedIds, IdFileName);
    //     if (!success)
    //     {
    //         Debug.LogError("Failed to save generated IDs.");
    //     }
    // }

    // private void LoadGeneratedIds()
    // {
    //     generatedIds = JsonFileHandler.LoadFromFile<List<int>>(IdFileName) ?? new List<int>();
    // }
    // #endregion
}