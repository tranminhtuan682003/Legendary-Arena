using System.Collections;
using TMPro;
using UnityEngine;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager Instance;
    private int score = 0;
    private int bestScore;
    private float speedPipe = 1f;
    private float speedSpawn = 2.5f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        FlappyBirdEventManager.OnGameStart += HandleStart;
        FlappyBirdEventManager.OnGameOver += HandleGameOver;

        // Tải bestScore từ PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void OnDisable()
    {
        FlappyBirdEventManager.OnGameStart -= HandleStart;
        FlappyBirdEventManager.OnGameOver -= HandleGameOver;
    }

    private void HandleStart()
    {
        isGameOver = false; // Đặt lại trạng thái game
        score = 0;
        speedPipe = 1f;
        speedSpawn = 2.5f;
        StartCoroutine(IncreaseSpeedOverTime()); // Tăng tốc độ mỗi 10 giây
        Debug.Log("Game started!");
    }

    private void HandleGameOver()
    {
        if (!isGameOver) // Chỉ xử lý nếu chưa game over
        {
            isGameOver = true;
            StopAllCoroutines(); // Dừng các coroutine khi game over
            SetBestScore();
            Debug.Log("Game over!");
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (!isGameOver) // Kiểm tra cờ để tiếp tục tăng tốc độ
        {
            yield return new WaitForSeconds(10f);
            speedPipe *= 1.1f;
            speedSpawn /= 1.1f;
            Debug.Log("Current pipe speed and curent speedSpawn: " + speedPipe + speedSpawn);
        }
    }

    public float GetCurrentPipeSpeed()
    {
        return speedPipe; // speedPipe là biến lưu tốc độ hiện tại của cột trong FlappyBirdGameManager
    }
    public float GetCurrentSpeedSpawnPipe()
    {
        return speedSpawn;
    }

    public void SetScore(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void SetBestScore()
    {
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore); // Lưu bestScore vào PlayerPrefs
            PlayerPrefs.Save(); // Lưu lại thay đổi ngay lập tức
        }
        Debug.Log("Best score is: " + bestScore);
    }

    public int GetBestScore()
    {
        return bestScore;
    }
}
