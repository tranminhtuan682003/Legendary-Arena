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

    public BetType SetResult(BetType betType)
    {
        return betType;
    }
}
