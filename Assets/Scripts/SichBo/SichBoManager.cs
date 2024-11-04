using System.Collections;
using UnityEngine;

public class SichBoManager : MonoBehaviour
{
    public static SichBoManager Instance;
    public int playerBalance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        UISBManager.Instance.countdownText.text = "Place your bets!";

        while (countdownTime > 0)
        {
            UISBManager.Instance.countdownText.text = $"{countdownTime}";
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
        while (countdownTime > 0 && !resultShown)
        {
            UISBManager.Instance.countdownText.text = $"{countdownTime}";
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

        UISBManager.Instance.countdownText.text = "Round Over";
        yield return new WaitForSeconds(10f);
        StartCoroutine(StartRound());
    }

    void ShowResult()
    {
        BetType result = Randomizer.Instance.GenerateRandomResult();
        BetResult betResult = (result == BetManager.Instance.currentBetType) ? BetResult.Win : BetResult.Lose;
        BetManager.Instance.CalculatePayout(betResult);
        UISBManager.Instance.ShowResult(betResult);
        SaveData();
    }

    private int fakePlayersOver = 10;
    private int fakePlayersUnder = 10;
    private int fakeBetMoneyOver = 100;
    private int fakeBetMoneyUnder = 100;
    private float growthFactor = 1.1f;

    void UpdateFakePlayerAndBet()
    {
        // Tăng trưởng theo cấp số nhân kết hợp yếu tố ngẫu nhiên
        fakePlayersOver = Mathf.RoundToInt(fakePlayersOver * growthFactor) + Random.Range(1, 10);
        fakePlayersUnder = Mathf.RoundToInt(fakePlayersUnder * growthFactor) + Random.Range(1, 10);

        fakeBetMoneyOver = Mathf.RoundToInt(fakeBetMoneyOver * growthFactor) + Random.Range(100, 10000);
        fakeBetMoneyUnder = Mathf.RoundToInt(fakeBetMoneyUnder * growthFactor) + Random.Range(100, 10000);

        // Hiển thị giá trị mới lên UI
        UISBManager.Instance.fakePlayerOver.text = fakePlayersOver.ToString();
        UISBManager.Instance.fakePlayerUnder.text = fakePlayersUnder.ToString();
        UISBManager.Instance.fakebetMoneyOver.text = fakeBetMoneyOver.ToString();
        UISBManager.Instance.fakebetMoneyUnder.text = fakeBetMoneyUnder.ToString();
    }




    public void UpdateScore(int amount)
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
