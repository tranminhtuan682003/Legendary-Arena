using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay1vs1Manager : MonoBehaviour
{
    public static GamePlay1vs1Manager Instance { get; private set; }

    public enum GameState { Start, Playing, GameOver }
    public GameState CurrentState { get; private set; }

    [Header("TurretManager")]
    [HideInInspector] public Transform targetOfTurret;

    [Header("SoldierManager")]
    [HideInInspector] public Transform targetOfSoldier;
    public GameObject soldierPrefab; // Prefab của lính
    public Transform soldierSpawnPoint; // Điểm xuất hiện của lính
    private int timerToSpawnSoldier = 60; // Mỗi phút sinh lính

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Start);

    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Start:
                StartGame();
                break;
            case GameState.Playing:
                PlayingGame();
                break;
            case GameState.GameOver:
                EndGame();
                break;
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Start");
        ChangeState(GameState.Playing);
    }

    public void PlayingGame()
    {
        Debug.Log("Game Playing");
        // StartCoroutine(SpawnSoldiersRoutine());
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        StopAllCoroutines();
    }

    private IEnumerator SpawnSoldiersRoutine()
    {
        while (CurrentState == GameState.Playing)
        {
            yield return new WaitForSeconds(timerToSpawnSoldier);
            for (int i = 0; i < 3; i++)
            {
                Instantiate(soldierPrefab, soldierSpawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
