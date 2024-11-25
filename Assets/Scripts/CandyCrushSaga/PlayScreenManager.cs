using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreenManager : MonoBehaviour
{
    public static PlayScreenManager Instance; // Singleton để dễ truy cập

    private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>(); // Tìm TextMeshProUGUI
        UpdateTextScore();
    }

    public void AddScore(int points)
    {
        currentScore += points; // Cộng thêm điểm
        UpdateTextScore();
    }

    private void UpdateTextScore()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
}
