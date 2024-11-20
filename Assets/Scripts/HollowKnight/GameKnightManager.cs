using UnityEngine;
using Zenject;

public class GameKnightManager : MonoBehaviour
{
    private SoundKnightManager soundKnightManager;
    [Inject]
    public void Construct(SoundKnightManager soundKnightManager)
    {
        this.soundKnightManager = soundKnightManager;
    }

    private void Start()
    {
    }
}
