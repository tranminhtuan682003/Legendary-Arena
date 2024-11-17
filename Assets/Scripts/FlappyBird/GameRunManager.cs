using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameRunManager : MonoBehaviour
{
    private float timeSpawn = 3f;
    private TextMeshProUGUI textScore;
    private bool isGameOver = false;
    private Coroutine spawnPipeCoroutine; // Tham chiếu đến Coroutine spawnPipe

    private void Awake()
    {
        textScore = GameObject.Find("TextScore")?.GetComponent<TextMeshProUGUI>();
    }



    private void Update()
    {
        if (textScore != null && FlappyBirdGameManager.Instance != null)
        {
            textScore.text = FlappyBirdGameManager.Instance.GetScore().ToString();
        }
        else
        {
            Debug.LogError("textScore or FlappyBirdGameManager.Instance is null.");
        }
    }

    private void OnEnable()
    {
        isGameOver = false;
        spawnPipeCoroutine = StartCoroutine(SpawnPipe()); // Khởi động lại Coroutine
        FlappyBirdEventManager.OnGameOver += HandleGameOver; // Đăng ký sự kiện
        FlappyBirdEventManager.TriggerGameStart();
        timeSpawn = FlappyBirdGameManager.Instance.GetCurrentSpeedSpawnPipe();
    }

    private void OnDisable()
    {
        FlappyBirdEventManager.OnGameOver -= HandleGameOver; // Hủy đăng ký sự kiện
    }

    private IEnumerator SpawnPipe()
    {
        while (!isGameOver)
        {
            UIFlappyManager.Instance.GetPipe();
            yield return new WaitForSeconds(timeSpawn);
        }
    }

    private void HandleGameOver()
    {
        isGameOver = true;
        if (spawnPipeCoroutine != null)
        {
            StopCoroutine(spawnPipeCoroutine); // Dừng chỉ Coroutine spawnPipe
        }
    }
}
