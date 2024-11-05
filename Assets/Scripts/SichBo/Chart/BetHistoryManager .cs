using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetHistoryManager : MonoBehaviour
{
    private Transform content;              // Content của ScrollView chứa các dòng lịch sử
    private ScrollRect scrollView;          // ScrollRect của ScrollView
    private GameObject historyItemPrefab;   // Prefab của item trong ScrollView

    private List<BetHistoryEntry> betHistory = new List<BetHistoryEntry>();

    private void Awake()
    {
        scrollView = GameObject.Find("ScrollViewChart").GetComponent<ScrollRect>();
        if (scrollView == null)
        {
            return;
        }

        content = scrollView.content; // Lấy Content từ ScrollRect
        if (content == null)
        {
            return;
        }

        historyItemPrefab = Resources.Load<GameObject>("Prefabs/HistoryItem");
    }

    // Phương thức gọi sau mỗi vòng chơi để thêm dòng lịch sử
    public void AddBetHistory(string sessionId, int betAmount, string isWin, int moneyChange)
    {
        BetHistoryEntry newEntry = new BetHistoryEntry(
            sessionId,
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            betAmount,
            isWin,
            moneyChange
        );

        betHistory.Add(newEntry);

        if (historyItemPrefab == null)
        {
            return;
        }

        GameObject newHistoryItem = Instantiate(historyItemPrefab, content);

        Transform sessionIdText = newHistoryItem.transform.Find("SeasionID/SessionIdText");
        if (sessionIdText != null)
            sessionIdText.GetComponent<TextMeshProUGUI>().text = newEntry.SessionId;
        else
            Debug.LogError("SessionIdText not found in HistoryItem prefab.");

        Transform dateText = newHistoryItem.transform.Find("Date/DateText");
        if (dateText != null)
            dateText.GetComponent<TextMeshProUGUI>().text = newEntry.Date;
        else
            Debug.LogError("DateText not found in HistoryItem prefab.");

        Transform betAmountText = newHistoryItem.transform.Find("TotalBet/BetAmountText");
        if (betAmountText != null)
            betAmountText.GetComponent<TextMeshProUGUI>().text = newEntry.BetAmount.ToString();
        else
            Debug.LogError("BetAmountText not found in HistoryItem prefab.");

        Transform statusText = newHistoryItem.transform.Find("State/StatusText");
        if (statusText != null)
            statusText.GetComponent<TextMeshProUGUI>().text = newEntry.IsWin;
        else
            Debug.LogError("StatusText not found in HistoryItem prefab.");

        Transform moneyChangeText = newHistoryItem.transform.Find("Detail/MoneyChangeText");
        if (moneyChangeText != null)
            moneyChangeText.GetComponent<TextMeshProUGUI>().text = (newEntry.MoneyChange > 0 ? "+" : "") + newEntry.MoneyChange.ToString();
        else
            Debug.LogError("MoneyChangeText not found in HistoryItem prefab.");

        scrollView.verticalNormalizedPosition = 0f; // Cuộn xuống cuối danh sách
    }
}