using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public static Randomizer Instance;

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

    public BetType GenerateRandomResult()
    {
        return (Random.Range(0, 2) == 0) ? BetType.Xiu : BetType.Tai;
    }
}
