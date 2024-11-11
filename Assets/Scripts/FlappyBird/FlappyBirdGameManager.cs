using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager Instance;
    private int score = 0;
    private int bestScore;
    private TextMeshProUGUI textScore;

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

    public void SetTextScore(TextMeshProUGUI textScore)
    {
        this.textScore = textScore;
        UpdateTextScore(); // Cập nhật ngay khi set để hiển thị đúng
    }

    public void SetScore(int score)
    {
        this.score += score;
        UpdateTextScore();
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        if (textScore != null)
        {
            textScore.text = score.ToString();
        }
    }

    public void SetBestScore(int bestScore)
    {
        this.bestScore = bestScore;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    private void UpdateTextScore()
    {
        if (textScore != null)
        {
            textScore.text = score.ToString();
        }
    }
}
