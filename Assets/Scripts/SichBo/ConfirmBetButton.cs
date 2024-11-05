using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBetButton : MonoBehaviour
{
    private Button button;
    public TypeConfirm typeConfirm;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        SoundSBManager.Instance.PlayConfirmSound();
        if (SichBoManager.Instance.playerBalance >= 100)
        {
            ProcessBet();

            if (typeConfirm == TypeConfirm.ALLIN)
            {
                HandleAllIn();
            }
            else if (typeConfirm == TypeConfirm.OK)
            {
                UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.currentBetAmount);
            }
            else
            {
                HandleCancel();
            }

            FinalizeBet();
        }
        else
        {
            HandleInsufficientFunds();
        }
    }

    private void ProcessBet()
    {
        SichBoManager.Instance.UpdateScoreAfterBet(BetManager.Instance.BetAmount());
    }

    private void HandleAllIn()
    {
        BetManager.Instance.ResetCurrentBetAmount();
        BetManager.Instance.SetCurrentBetAmount(SichBoManager.Instance.playerBalance);
        BetManager.Instance.SaveBetAmount(BetManager.Instance.CurrentBetAmount());
        SichBoManager.Instance.UpdateScoreAfterBet(-BetManager.Instance.BetAmount());
        UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.currentBetAmount);
    }

    private void HandleCancel()
    {
        SichBoManager.Instance.UpdateScoreAfterBet(BetManager.Instance.BetAmount());
        UISBManager.Instance.ResetConfirmBetAmountDisplay();
        BetManager.Instance.ResetBetAmount();
        UISBManager.Instance.RefeshUI();
    }

    private void FinalizeBet()
    {
        UISBManager.Instance.ChangeStateOverBetButton(false);
        UISBManager.Instance.ChangeStateUnderBetButton(false);
        BetManager.Instance.SaveBetAmount(BetManager.Instance.CurrentBetAmount());
        SichBoManager.Instance.UpdateScoreAfterBet(-BetManager.Instance.BetAmount());
        BetManager.Instance.ResetCurrentBetAmount();
        UISBManager.Instance.ResetBetAmountDisplay();
        UISBManager.Instance.StateBetTable(false);
    }

    private void HandleInsufficientFunds()
    {
        UISBManager.Instance.ResetBetAmountDisplay();
        UISBManager.Instance.StateBetTable(false);
        Debug.Log("No Enough Money");
    }
}
