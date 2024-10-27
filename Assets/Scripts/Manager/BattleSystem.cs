using Zenject;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Inject] private UIManager uIManager; // Zenject tự động inject GameManager

    public void StartBattle()
    {
        uIManager.InitializeBattle();
        Debug.Log("Battle started!");
    }
}
