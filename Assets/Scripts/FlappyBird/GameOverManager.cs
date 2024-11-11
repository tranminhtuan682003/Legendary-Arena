using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private Button homeButton;
    private void Awake()
    {
        InitLize();
    }
    private void Start()
    {
        homeButton.onClick.AddListener(HandleClick);
    }

    private void InitLize()
    {
        homeButton = GameObject.Find("Home").GetComponent<Button>();
    }

    private void HandleClick()
    {
        UIFlappyManager.Instance.ChangeStateScreenPlayAgain(true);
        UIFlappyManager.Instance.ChangeStateScreenGameOver(false);
    }
}
