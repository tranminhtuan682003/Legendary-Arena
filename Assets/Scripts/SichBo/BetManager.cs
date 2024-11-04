using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Instance;
    public int currentBetAmount;
    public int betAmount;
    public BetType currentBetType;

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
    public void SaveBetMoney(int amount)
    {
        betAmount = amount;
    }

    public void SetBetMoney(int amount)
    {
        currentBetAmount += amount;
        UISBManager.Instance.UpdateBetAmountDisplay(currentBetAmount);
    }

    public void ResetBets()
    {
        currentBetAmount = 0;
    }

    public void CalculatePayout(BetResult result)
    {
        int payout = (result == BetResult.Win) ? betAmount * 2 : -betAmount;
        SichBoManager.Instance.UpdateScore(payout);
    }
}
