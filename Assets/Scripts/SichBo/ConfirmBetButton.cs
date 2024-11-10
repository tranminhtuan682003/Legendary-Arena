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
        if (SichBoManager.Instance.playerBalance >= 100 && BetManager.Instance.currentBetAmount <= SichBoManager.Instance.playerBalance)
        {
            if (typeConfirm == TypeConfirm.ALLIN)
            {
                HandleAllIn();
            }
            else if (typeConfirm == TypeConfirm.OK)
            {
                HandleOK();
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

    private void HandleAllIn()
    {
        BetManager.Instance.ResetCurrentBetAmount();
        BetManager.Instance.SetCurrentBetAmount(SichBoManager.Instance.playerBalance);
        BetManager.Instance.SaveBetAmount(BetManager.Instance.CurrentBetAmount());
        SichBoManager.Instance.UpdateScoreAfterBet(-BetManager.Instance.BetAmount());
        UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.BetAmount());
    }

    private void HandleOK()
    {
        SichBoManager.Instance.UpdateScoreAfterBet(BetManager.Instance.BetAmount());
        BetManager.Instance.SaveBetAmount(BetManager.Instance.CurrentBetAmount());
        SichBoManager.Instance.UpdateScoreAfterBet(-BetManager.Instance.BetAmount());
        UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.BetAmount());
    }

    private void HandleCancel()
    {
        BetManager.Instance.currentBetType = BetType.None;
        SichBoManager.Instance.UpdateScoreAfterBet(BetManager.Instance.BetAmount());
        UISBManager.Instance.ResetConfirmBetAmountDisplay();
        UISBManager.Instance.RefeshUI();
    }

    private void FinalizeBet()
    {
        UISBManager.Instance.ChangeStateOverBetButton(false);
        UISBManager.Instance.ChangeStateUnderBetButton(false);
        BetManager.Instance.ResetCurrentBetAmount();
        UISBManager.Instance.ResetBetAmountDisplay();
        UISBManager.Instance.StateBetTable(false);
    }

    private void HandleInsufficientFunds()
    {
        UISBManager.Instance.ResetBetAmountDisplay();
        BetManager.Instance.currentBetType = BetType.None;
        BetManager.Instance.ResetBetAmount();
        UISBManager.Instance.ChangeStateOverBetButton(false);
        UISBManager.Instance.ChangeStateUnderBetButton(false);
        UISBManager.Instance.StateBetTable(false);

        Debug.Log("No Enough Money");
    }
}
