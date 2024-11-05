using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetTypeButton : MonoBehaviour
{
    private Button button;
    public BetType betType;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (betType == BetType.Tai)
        {
            UISBManager.Instance.ChangeStateOverBetButton(true);
            UISBManager.Instance.ChangeStateUnderBetButton(false);
        }
        else
        {
            UISBManager.Instance.ChangeStateUnderBetButton(true);
            UISBManager.Instance.ChangeStateOverBetButton(false);
        }

        BetManager.Instance.currentBetType = betType;
        UISBManager.Instance.StateBetTable(true);
        Debug.Log(BetManager.Instance.currentBetType);
    }

}
