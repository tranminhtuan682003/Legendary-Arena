using System;
using UnityEngine;

public class GameSBEventManager : MonoBehaviour
{
    public static GameSBEventManager Instance;

    // Các sự kiện trong game
    public event Action OnDragDistanceReached;
    public event Action<bool> OnViewResultChanged;

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

    // Phương thức để kích hoạt sự kiện OnDragDistanceReached
    public void TriggerDragDistanceReached()
    {
        OnDragDistanceReached?.Invoke();
    }

    // Phương thức để kích hoạt sự kiện OnViewResultChanged
    public void TriggerViewResultChanged(bool result)
    {
        OnViewResultChanged?.Invoke(result);
    }
}
