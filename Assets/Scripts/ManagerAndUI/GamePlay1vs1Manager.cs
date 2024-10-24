using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay1vs1Manager : MonoBehaviour
{
    public static GamePlay1vs1Manager Instance { get; private set; }

    public enum GameState { Start, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    #region TurretManager
    public Transform target;

    #endregion

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
                Debug.Log("Game Start");
                break;
            case GameState.Playing:
                Debug.Log("Game Playing");
                break;
            case GameState.Paused:
                Debug.Log("Game Paused");
                break;
            case GameState.GameOver:
                Debug.Log("Game Over");
                break;
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    public void EndGame()
    {
        ChangeState(GameState.GameOver);
    }
}
