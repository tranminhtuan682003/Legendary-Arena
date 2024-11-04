using UnityEngine;
using UnityEngine.UI;

public class BetButtonSB : MonoBehaviour
{
    public int betAmount;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnBetButtonClicked);
    }

    private void OnBetButtonClicked()
    {
        BetManager.Instance.SetBetMoney(betAmount);
        SoundSBManager.Instance.PlayBetSound();
    }
}
