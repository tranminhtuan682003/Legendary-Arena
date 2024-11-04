using System.Collections;
using System.Collections.Generic;
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
        if (SichBoManager.Instance.playerBalance >= 100)
        {
            if (typeConfirm == TypeConfirm.ALLIN)
            {
                BetManager.Instance.ResetBets();
                BetManager.Instance.SetBetMoney(SichBoManager.Instance.playerBalance);
                UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.currentBetAmount);
            }
            else if (typeConfirm == TypeConfirm.OK)
            {
                UISBManager.Instance.UpdateConfirmBetAmountDisplay(BetManager.Instance.currentBetAmount);
            }
            else
            {
                UISBManager.Instance.ResetConfirmBetAmountDisplay();
            }
            BetManager.Instance.SaveBetMoney(BetManager.Instance.currentBetAmount);
            BetManager.Instance.ResetBets();
            UISBManager.Instance.ResetBetAmountDisplay();
            UISBManager.Instance.betTable.SetActive(false);
        }
        else
        {
            UISBManager.Instance.ResetBetAmountDisplay();
            UISBManager.Instance.betTable.SetActive(false);
            Debug.Log("No Enoungh Money");
        }

    }
}
