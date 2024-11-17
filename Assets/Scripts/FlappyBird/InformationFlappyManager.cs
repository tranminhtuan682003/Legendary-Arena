using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationFlappyManager : MonoBehaviour
{
    private Button exit;
    private TextMeshProUGUI bestScore;
    private void Awake()
    {
        Initlize();
    }
    void Start()
    {
        exit.onClick.AddListener(HandleClick);
    }

    private void OnEnable()
    {
        DisplayScore();
    }

    private void Initlize()
    {
        exit = GameObject.Find("Exit").GetComponent<Button>();
        bestScore = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
    }

    private void HandleClick()
    {
        UIFlappyManager.Instance.ChangeStateInformationScreen(false);
    }
    private void DisplayScore()
    {
        bestScore.text = "Hihi";
        bestScore.text = FlappyBirdGameManager.Instance.GetBestScore().ToString();
    }

}
