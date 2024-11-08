using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SichBoManager : MonoBehaviour
{
    public static SichBoManager Instance;
    public BetHistoryManager betHistoryManager;
    public int playerBalance;

    private int detailBet;
    private bool viewResult;
    private bool openBowl;
    private int fakePlayersOver = 10;
    private int fakePlayersUnder = 10;
    private int fakeBetMoneyOver = 100;
    private int fakeBetMoneyUnder = 100;
    private float growthFactor = 1.1f;
    private IDiceRollStrategy diceRollStrategy;

    private List<(int, int, int)> casesGreaterThan10;
    private List<(int, int, int)> casesLessThanOrEqual10;
    private DiceCaseGenerator generator;

    private BetType result;


    #region Initialization
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
        LoadData();
        StartCoroutine(StartRound());
        GameSBEventManager.Instance.OnDragDistanceReached += HandleDragDistanceReached;
    }

    private void OnDestroy()
    {
        if (GameSBEventManager.Instance != null)
        {
            GameSBEventManager.Instance.OnDragDistanceReached -= HandleDragDistanceReached;
        }
    }


    public void SetDiceRollStrategy(IDiceRollStrategy strategy)
    {
        diceRollStrategy = strategy;
    }

    public (int, int, int) RollDice()
    {
        if (diceRollStrategy == null)
        {
            Debug.LogError("Dice roll strategy has not been set!");
            return (0, 0, 0); // Giá trị mặc định hoặc xử lý khác
        }
        return diceRollStrategy.RollDice();
    }


    private void HandleDragDistanceReached()
    {
        openBowl = true;
        ShowResult();
    }
    #endregion

    #region Properties
    public bool ViewResult
    {
        get => viewResult;
        set
        {
            if (viewResult != value)
            {
                viewResult = value;
                GameSBEventManager.Instance.TriggerViewResultChanged(viewResult);
            }
        }
    }
    #endregion

    #region Game Flow
    public IEnumerator StartRound()
    {
        int countdownTime = 30;
        while (countdownTime > 0)
        {
            UISBManager.Instance.UpdateCountdownText(countdownTime);
            UpdateFakePlayerAndBet();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        StartCoroutine(ViewResultCoroutine());
    }

    private IEnumerator ViewResultCoroutine()
    {
        SetDice();
        ViewResult = true;
        int countdownTime = 15;
        float remainingTime = 0f;
        bool resultShown = false;
        UISBManager.Instance.ChangeAnimationTimer(true, "Run", "Idle");
        UISBManager.Instance.ChanAnimationFakeUI(false, "Run", "Idle");

        while (countdownTime > 0 && !resultShown)
        {
            UISBManager.Instance.UpdateCountdownText(countdownTime);

            if (openBowl)
            {
                // Tính toán thời gian còn lại khi thoát khỏi vòng lặp
                remainingTime = countdownTime;
                break;
            }
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        if (!openBowl)
        {
            ShowResult();
            UISBManager.Instance.ChangeAnimationBowl(true, "Run", "Idle", true);
        }

        UISBManager.Instance.UpdateCountdownText(0);
        StartCoroutine(NewRound(remainingTime));
    }

    #endregion

    #region Result Handling

    private void SetDice()
    {
        result = ManipulateResults();
        if (result == BetType.Tai)
        {
            SetDiceRollStrategy(new TaiDiceRollStrategy(casesGreaterThan10));
        }
        else
        {
            SetDiceRollStrategy(new XiuDiceRollStrategy(casesLessThanOrEqual10));
        }

        var diceResult = RollDice(); // Lưu trữ kết quả để sử dụng nếu cần
        UISBManager.Instance.SetImageDice(diceResult.Item1, diceResult.Item2, diceResult.Item3);
        Debug.Log($"Kết quả xúc xắc: {diceResult.Item1}, {diceResult.Item2}, {diceResult.Item3}");
    }
    private void ShowResult()
    {
        UISBManager.Instance.ActiveOverButton(false);
        UISBManager.Instance.ActiveUnderButton(false);
        if (result == BetType.Tai)
        {
            UISBManager.Instance.ChangeAnimationNameResult(true, true);
        }
        else
        {
            UISBManager.Instance.ChangeAnimationNameResult(true, false);
        }
        BetResult betResult = (result == BetManager.Instance.currentBetType) ? BetResult.Win : BetResult.Lose;
        BetManager.Instance.CalculatePayout(betResult);

        int detailBet = (betResult == BetResult.Win) ? BetManager.Instance.betAmount : -BetManager.Instance.betAmount;
        string isWinText = (betResult == BetResult.Win) ? "Tài" : "Xỉu";

        if (BetManager.Instance.currentBetType != BetType.None)
        {
            betHistoryManager.AddBetHistory(UISBManager.Instance.idGame.text, BetManager.Instance.betAmount, isWinText, detailBet);
        }

        UISBManager.Instance.ShowResult(betResult);
        SaveData();
    }

    #endregion

    #region Fake Data Update
    private void UpdateFakePlayerAndBet()
    {
        fakePlayersOver = Mathf.RoundToInt(fakePlayersOver * growthFactor) + Random.Range(1, 10);
        fakePlayersUnder = Mathf.RoundToInt(fakePlayersUnder * growthFactor) + Random.Range(1, 10);

        fakeBetMoneyOver = Mathf.RoundToInt(fakeBetMoneyOver * growthFactor) + Random.Range(100, 10000);
        fakeBetMoneyUnder = Mathf.RoundToInt(fakeBetMoneyUnder * growthFactor) + Random.Range(100, 10000);

        UISBManager.Instance.UpdateFakePlayerAndBet(fakePlayersOver, fakePlayersUnder, fakeBetMoneyOver, fakeBetMoneyUnder);
    }

    private BetType ManipulateResults()
    {
        if (fakeBetMoneyOver > fakeBetMoneyUnder)
        {
            return Randomizer.Instance.SetResult(BetType.Xiu);
        }
        else if (fakeBetMoneyOver < fakeBetMoneyUnder)
        {
            return Randomizer.Instance.SetResult(BetType.Tai);
        }
        else
        {
            Debug.Log("Fake bet money is equal, choosing a random result.");
            return Random.value > 0.5f ? BetType.Tai : BetType.Xiu;
        }
    }

    #endregion

    #region Round Management
    private IEnumerator NewRound(float additionalTime)
    {
        float initialWaitTime = 7.5f + additionalTime;
        yield return new WaitForSeconds(initialWaitTime);
        UISBManager.Instance.ChangeAnimationDice(true, "Run", "Idle");
        UISBManager.Instance.ChangeAnimationNameResult(false, true);
        UISBManager.Instance.ChangeAnimationNameResult(false, false);
        UISBManager.Instance.ResetResultText();
        UISBManager.Instance.RefeshUI();

        yield return new WaitForSeconds(2.5f);

        openBowl = false;
        ViewResult = false;
        fakePlayersOver = 10;
        fakePlayersUnder = 10;
        fakeBetMoneyOver = 100;
        fakeBetMoneyUnder = 100;
        UISBManager.Instance.ChangeAnimationBowl(false, "Run", "Idle", true);
        UISBManager.Instance.ChanAnimationFakeUI(true, "Run", "Idle");
        UISBManager.Instance.ChangeAnimationTimer(false, "Run", "Idle");
        UISBManager.Instance.ChangeAnimationDice(false, "Run", "Idle");
        UISBManager.Instance.ActiveOverButton(true);
        UISBManager.Instance.ActiveUnderButton(true);
        BetManager.Instance.ResetBetAmount();

        StartCoroutine(StartRound());
    }


    #endregion

    #region Score and Data Management
    public void UpdateScoreAfterRound(int amount)
    {
        playerBalance += amount;
        UISBManager.Instance.UpdatePlayerBalance(playerBalance);
    }

    public void SetDetailBet(int amount)
    {
        detailBet = amount;
    }

    public void UpdateScoreAfterBet(int amount)
    {
        playerBalance += amount;
        UISBManager.Instance.UpdatePlayerBalance(playerBalance);
    }

    private void SaveData()
    {
        DataManager.Instance.SavePlayerData(playerBalance);
    }

    private void LoadData()
    {
        generator = new DiceCaseGenerator();
        casesGreaterThan10 = generator.GenerateCasesGreaterThan10();
        casesLessThanOrEqual10 = generator.GenerateCasesLessThanOrEqual10();
        playerBalance = DataManager.Instance.LoadPlayerData();
        UISBManager.Instance.UpdatePlayerBalance(playerBalance);
    }
    #endregion
}
