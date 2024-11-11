using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameRunManager : MonoBehaviour
{
    private float timeSpawn = 2.5f;
    private TextMeshProUGUI textScore;

    void Awake()
    {
        textScore = GameObject.Find("TextScore")?.GetComponent<TextMeshProUGUI>();
        if (textScore == null)
        {
            Debug.LogError("TextScore object not found or missing TextMeshProUGUI component.");
        }
    }

    void Start()
    {
        StartCoroutine(SpawnPipe());
        if (textScore != null && FlappyBirdGameManager.Instance != null)
        {
            FlappyBirdGameManager.Instance.SetTextScore(textScore);
        }
    }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            UIFlappyManager.Instance.GetPipe();
            yield return new WaitForSeconds(timeSpawn);
        }
    }
}
