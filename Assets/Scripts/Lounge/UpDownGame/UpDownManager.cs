using System.Collections;
using TMPro;
using UnityEngine;

public class UpDownManager : MonoBehaviour
{
    // Singleton instance
    private static UpDownManager instance;
    public static UpDownManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UpDownManager>();
                if (instance == null)
                {
                    var newObj = new GameObject("UpDownManager");
                    instance = newObj.AddComponent<UpDownManager>();
                }
            }
            return instance;
        }
    }

    // Timer variables
    private const float InitialTimeLeft = 20f;
    private const float TimeBeforeNewSession = 10f;
    private float timeLeft = InitialTimeLeft;
    private float timeOpenBowl = 10f;

    // UI Elements
    [Header("UI Elements")]
    [HideInInspector] public GameObject footer;
    [HideInInspector] public GameObject chart;
    private TextMeshProUGUI timer;
    [HideInInspector] public TextMeshProUGUI asset;
    [HideInInspector] public int totalAsset;
    [HideInInspector] public int totalPreviousAsset;

    private RectTransform bowl;

    // show bet money
    [HideInInspector] public TextMeshProUGUI moneyBetOver;
    [HideInInspector] public TextMeshProUGUI moneyBetUnder;
    [HideInInspector] public TextMeshProUGUI moneyBetOver2;
    [HideInInspector] public TextMeshProUGUI moneyBetUnder2;
    // private int bet

    // Bet Information
    [HideInInspector] public string nameSideBet;
    [HideInInspector] public string moneyBet;
    [HideInInspector] public int totalBetMoney;
    [HideInInspector] public int resultBetMoney;


    // Player Info
    [Header("Player Information and total money bet information")]
    [HideInInspector] public TextMeshProUGUI totalPlayerUnder;
    [HideInInspector] public TextMeshProUGUI totalPlayerOver;
    private int numberPlayerUnder;
    private int numberPlayerOver;
    [HideInInspector] public TextMeshProUGUI totalBetMoneyUnder;
    [HideInInspector] public TextMeshProUGUI totalBetMoneyOver;
    private int numberBetMoneyUnder;
    private int numberBetMoneyOver;
    private bool canRandomPlayer;

    // Wealth-based behavior
    private float playerWealth = 100000f;
    private int result;

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        InitializeComponents();
        ResetUIElements();
        InitializeAsset();
        StartCoroutine(TimerCoroutine());
        StartCoroutine(RandomNumberPlayerCoroutine());
    }

    private void InitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeComponents()
    {
        timer = FindComponentInChild<TextMeshProUGUI>("BetTable/Timer");
        footer = FindComponentInChild<Transform>("Footer").gameObject;
        chart = FindComponentInChild<Transform>("Chart").gameObject;
        bowl = FindComponentInChild<RectTransform>("BetTable/Bowl");
        asset = FindComponentInChild<TextMeshProUGUI>("Header/Area/CoinAndDiamond/Coin/Asset");

        moneyBetOver = FindComponentInChild<TextMeshProUGUI>("BetTable/Over/Bet/Area/BetOverButton/MoneyBetOver");
        moneyBetUnder = FindComponentInChild<TextMeshProUGUI>("BetTable/Under/Bet/Area/BetUnderButton/MoneyBetUnder");
        moneyBetOver2 = FindComponentInChild<TextMeshProUGUI>("BetTable/Over/Bet/MoneyBet/MoneyBetOver2");
        moneyBetUnder2 = FindComponentInChild<TextMeshProUGUI>("BetTable/Under/Bet/MoneyBet/MoneyBetUnder2");

        totalPlayerUnder = FindComponentInChild<TextMeshProUGUI>("BetTable/Under/TotalPlayer/Panel/TotalPlayerUnder");
        totalPlayerOver = FindComponentInChild<TextMeshProUGUI>("BetTable/Over/TotalPlayer/Panel/TotalPlayerOver");

        totalBetMoneyOver = FindComponentInChild<TextMeshProUGUI>("BetTable/Over/Money/TotalBetMoneyOver");
        totalBetMoneyUnder = FindComponentInChild<TextMeshProUGUI>("BetTable/Under/Money/TotalBetMoneyUnder");
    }

    private T FindComponentInChild<T>(string path) where T : Component
    {
        var component = transform.Find(path)?.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"{typeof(T).Name} component at path '{path}' not found.");
        }
        return component;
    }

    private void ResetUIElements()
    {
        footer.SetActive(false);
        chart.SetActive(false);
        moneyBetOver2.text = string.Empty;
        moneyBetUnder2.text = string.Empty;
        canRandomPlayer = true;
    }

    private void InitializeAsset()
    {
        asset.text = "686868";
        totalAsset = int.Parse(asset.text);
        totalPreviousAsset = totalAsset;
    }

    private IEnumerator TimerCoroutine()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerUI(timeLeft);
            yield return null;
        }
        EndTimer();
    }

    private void UpdateTimerUI(float time)
    {
        timer.text = Mathf.Ceil(time).ToString();
    }

    private void EndTimer()
    {
        canRandomPlayer = false;
        timeOpenBowl = 10f;
        result = Random.Range(3, 18);
        StartCoroutine(TimerOpenBowlCoroutine());
        Debug.Log("Timer ended. Random player stopped.");
    }

    private IEnumerator TimerOpenBowlCoroutine()
    {
        while (timeOpenBowl > 0)
        {
            timeOpenBowl -= Time.deltaTime;
            UpdateTimerUI(timeOpenBowl);
            yield return null;
        }
        timer.text = string.Empty;
        ResultSeasion();
        StartNewSession();
    }

    private void StartNewSession()
    {
        // Reset random values to 0
        ResetRandomValues();
        StartCoroutine(NewSessionCoroutine());
    }

    private void ResetRandomValues()
    {
        numberBetMoneyOver = 0;
        numberBetMoneyUnder = 0;
        numberPlayerOver = 0;
        numberPlayerUnder = 0;

        UpdateUIWithResetValues();
    }

    private void UpdateUIWithResetValues()
    {
        totalPlayerOver.text = "0";
        totalPlayerUnder.text = "0";
        totalBetMoneyOver.text = "0";
        totalBetMoneyUnder.text = "0";
    }

    private IEnumerator NewSessionCoroutine()
    {
        yield return new WaitForSeconds(TimeBeforeNewSession);
        canRandomPlayer = true;
        timeLeft = InitialTimeLeft;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator RandomNumberPlayerCoroutine()
    {
        while (true)
        {
            if (canRandomPlayer)
            {
                ApplyRealisticBettingStrategies();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void ApplyRealisticBettingStrategies()
    {
        // Sử dụng mô phỏng dựa trên tài sản của người chơi
        float betAmountOver = SimulateWealthBasedBet();
        float betAmountUnder = SimulateWealthBasedBet();

        numberBetMoneyOver += Mathf.FloorToInt(betAmountOver);
        numberBetMoneyUnder += Mathf.FloorToInt(betAmountUnder);

        // Random số lượng người chơi cho mỗi lần random
        numberPlayerOver += Random.Range(1, 20);
        numberPlayerUnder += Random.Range(1, 20);

        // Cập nhật giao diện người dùng
        UpdateUIWithRandomValues();
    }

    private float SimulateWealthBasedBet()
    {
        // Áp dụng chiến lược cược dựa trên tài sản của người chơi, cược từ 1% đến 10% tài sản
        return playerWealth * Random.Range(0.005f, 0.1f);
    }

    private void UpdateUIWithRandomValues()
    {
        totalBetMoneyOver.text = numberBetMoneyOver.ToString();
        totalBetMoneyUnder.text = numberBetMoneyUnder.ToString();
        totalPlayerOver.text = numberPlayerOver.ToString();
        totalPlayerUnder.text = numberPlayerUnder.ToString();
    }

    private void ResultSeasion()
    {
        int currentMoney = resultBetMoney;
        if (result > 10 && nameSideBet == "BetOverButton")
        {
            currentMoney += resultBetMoney - resultBetMoney * 4 / 100;
            Debug.Log("Tai ban thang");
        }
        else if (result <= 10 && nameSideBet == "BetUnderButton")
        {
            currentMoney += resultBetMoney - resultBetMoney * 4 / 100; ;
            Debug.Log("Xiu ban thang");
        }
        else if (result > 10 && nameSideBet == "BetUnderButton")
        {
            currentMoney = 0;
            Debug.Log("Tai ban thua");
        }
        else if (result <= 10 && nameSideBet == "BetOverButton")
        {
            currentMoney = 0;
            Debug.Log("Xiu ban thua");
        }
        totalAsset += currentMoney;
        totalPreviousAsset = totalAsset;
        asset.text = totalPreviousAsset.ToString();
        Debug.Log($"Updated asset.text: {asset.text}");

        // Đặt lại các giá trị cho phiên chơi tiếp theo
        resultBetMoney = 0;
        nameSideBet = string.Empty;
        moneyBetOver2.text = string.Empty;
        moneyBetUnder2.text = string.Empty;
    }

}
