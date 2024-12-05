using System.Collections;
using UnityEngine;
using Zenject;

public class GameKnightManager : MonoBehaviour
{
    [Inject] private SoundKnightManager soundKnightManager;
    [Inject] private UIKnightManager uIKnightManager;
    // [Inject] private KnightPlayScreenManager knightPlayScreenManager;
    private Transform knight;

    private void OnEnable()
    {
        KnightEventManager.OnGameWin += HandleGameWin;
        KnightEventManager.OnPlayerDead += HandlePlayerDead;
        KnightEventManager.OnKnightEnable += ActiveKnight;
    }

    private void Start()
    {
    }

    private void HandleGameWin()
    {
        Debug.Log("Game Win event triggered");
        soundKnightManager.PlayMusicVictory();
        // knightPlayScreenManager.Win();
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(5f);
        uIKnightManager.ChangeStateStartScreen(true);
        uIKnightManager.ChangeStatePlayScreen(false);
    }

    private void HandlePlayerDead()
    {
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(10f);
        uIKnightManager.CreateKnight();
    }

    private void ActiveKnight(Transform knight)
    {
    }

    private void OnDisable()
    {
        KnightEventManager.OnGameWin -= HandleGameWin;
        KnightEventManager.OnPlayerDead -= HandlePlayerDead;
        KnightEventManager.OnKnightEnable -= ActiveKnight;
    }
}
