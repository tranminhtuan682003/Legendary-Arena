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
        BetManager.Instance.currentBetType = betType;
        UISBManager.Instance.betTable.SetActive(true);
        Debug.Log(BetManager.Instance.currentBetType);
    }
}
