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
    public void SaveBetAmount(int amount)
    {
        betAmount = amount;
    }
    public int BetAmount()
    {
        return betAmount;
    }

    public void SetCurrentBetAmount(int amount)
    {
        currentBetAmount += amount;
        UISBManager.Instance.UpdateBetAmountDisplay(currentBetAmount);
    }
    public int CurrentBetAmount()
    {
        return currentBetAmount;
    }

    public void ResetCurrentBetAmount()
    {
        currentBetAmount = 0;
    }

    public void ResetBetAmount()
    {
        betAmount = 0;
        currentBetType = BetType.None;
    }

    public void CalculatePayout(BetResult result)
    {
        if (currentBetType == BetType.None)
        {
            SoundSBManager.Instance.PlayNoneBet();
        }
        else
        {
            if (result == BetResult.Win)
            {
                SoundSBManager.Instance.PlayWinSound();
            }
            else
            {
                SoundSBManager.Instance.PlayLoseSound();
            }
        }

        int payout = (result == BetResult.Win) ? betAmount * 2 : -0;
        SichBoManager.Instance.UpdateScoreAfterRound(payout);
        SichBoManager.Instance.SetDetailBet(payout);
    }

}
