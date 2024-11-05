using System.Collections;
using UnityEngine;

public class SichBoManager : MonoBehaviour
{
    public static SichBoManager Instance;
    public BetHistoryManager betHistoryManager;
    public int playerBalance;

    private int detailBet;

    private int fakePlayersOver = 10;
    private int fakePlayersUnder = 10;
    private int fakeBetMoneyOver = 100;
    private int fakeBetMoneyUnder = 100;
    private float growthFactor = 1.1f;

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
    }

    public IEnumerator StartRound()
    {
        int countdownTime = 10;
        while (countdownTime > 0)
        {
            UISBManager.Instance.UpdateCountdownText(countdownTime);
            UpdateFakePlayerAndBet();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        StartCoroutine(ViewResult());
    }

    IEnumerator ViewResult()
    {
        int countdownTime = 10;
        bool resultShown = false;
        UISBManager.Instance.ChangeAnimationTimer("ShowResult");
        while (countdownTime > 0 && !resultShown)
        {
            UISBManager.Instance.UpdateCountdownText(countdownTime);
            if (Input.GetMouseButtonDown(0))
            {
                ShowResult();
                resultShown = true;
                break;
            }

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        if (!resultShown)
        {
            ShowResult();
        }

        UISBManager.Instance.UpdateCountdownText(0);
        yield return new WaitForSeconds(10f);
        UISBManager.Instance.ChangeAnimationTimer("Idle");
        NewRound();
        StartCoroutine(StartRound());
    }

    void ShowResult()
    {
        UISBManager.Instance.ActiveOverButton(false);
        UISBManager.Instance.ActiveUnderButton(false);
        BetType result = Randomizer.Instance.GenerateRandomResult();
        BetResult betResult = (result == BetManager.Instance.currentBetType) ? BetResult.Win : BetResult.Lose;
        BetManager.Instance.CalculatePayout(betResult);

        int detailBet = (betResult == BetResult.Win) ? BetManager.Instance.betAmount : -BetManager.Instance.betAmount;
        string isWinText = (betResult == BetResult.Win) ? "Win" : "Lose";
        if (BetManager.Instance.currentBetType != BetType.None)
        {
            betHistoryManager.AddBetHistory(UISBManager.Instance.idGame.text, BetManager.Instance.betAmount, isWinText, detailBet);
        }
        UISBManager.Instance.ShowResult(betResult);
        SaveData();
    }


    private void UpdateFakePlayerAndBet()
    {
        // Tăng trưởng theo cấp số nhân kết hợp yếu tố ngẫu nhiên
        fakePlayersOver = Mathf.RoundToInt(fakePlayersOver * growthFactor) + Random.Range(1, 10);
        fakePlayersUnder = Mathf.RoundToInt(fakePlayersUnder * growthFactor) + Random.Range(1, 10);

        fakeBetMoneyOver = Mathf.RoundToInt(fakeBetMoneyOver * growthFactor) + Random.Range(100, 10000);
        fakeBetMoneyUnder = Mathf.RoundToInt(fakeBetMoneyUnder * growthFactor) + Random.Range(100, 10000);

        // Hiển thị giá trị mới lên UI
        UISBManager.Instance.UpdateFakePlayerAndBet(fakePlayersOver, fakePlayersUnder, fakeBetMoneyOver, fakeBetMoneyUnder);
    }

    private void NewRound()
    {
        fakePlayersOver = 10;
        fakePlayersUnder = 10;
        fakeBetMoneyOver = 100;
        fakeBetMoneyUnder = 100;
        UISBManager.Instance.RefeshUI();
        BetManager.Instance.ResetBetAmount();
        UISBManager.Instance.ActiveOverButton(true);
        UISBManager.Instance.ActiveUnderButton(true);

    }

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
        playerBalance = DataManager.Instance.LoadPlayerData();
        UISBManager.Instance.UpdatePlayerBalance(playerBalance);
    }
}
