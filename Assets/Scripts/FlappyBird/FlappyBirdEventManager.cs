using System;
using UnityEngine;

public class FlappyBirdEventManager : MonoBehaviour
{
    // Sự kiện OnGameStart để các script khác có thể đăng ký
    public static event Action OnGameStart;
    public static event Action OnGameOver;

    // Phương thức để kích hoạt sự kiện OnGameStart
    public static void TriggerGameStart()
    {
        OnGameStart?.Invoke(); // Kiểm tra nếu có script nào đăng ký sự kiện thì gọi
    }

    public static void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }
}
