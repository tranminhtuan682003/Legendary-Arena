using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartKnightScreenManager : MonoBehaviour
{
    private Button playGame;

    private void Awake()
    {

    }
    private void Start()
    {
        playGame.onClick.AddListener(HandleClick);
    }

    private void InitLize()
    {
        playGame = GetComponent<Button>();
    }

    private void HandleClick()
    {
        UIKnightManager.instance.ChangeStateStartScreen(true);
    }
}
