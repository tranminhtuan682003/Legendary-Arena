using UnityEngine;
using Zenject;

public class GameKnightManager : MonoBehaviour
{
    [Inject] private SoundKnightManager soundKnightManager;

    private void Start()
    {
    }
}
