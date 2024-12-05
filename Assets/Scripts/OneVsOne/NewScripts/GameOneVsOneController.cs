using UnityEngine;
using Zenject;

public class GameOneVsOneController : MonoBehaviour
{
    [Inject] private SpawnHeroSystem spawnHeroSystem;

    private void OnEnable()
    {
    }

    private void SpawnHero()
    {
        spawnHeroSystem.CreateHeroByName("Valhein");
    }
}
