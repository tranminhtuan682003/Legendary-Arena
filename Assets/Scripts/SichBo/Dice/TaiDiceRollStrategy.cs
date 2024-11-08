using System.Collections.Generic;
using UnityEngine;

public class TaiDiceRollStrategy : IDiceRollStrategy
{
    private List<(int, int, int)> casesGreaterThan10;

    public TaiDiceRollStrategy(List<(int, int, int)> cases)
    {
        casesGreaterThan10 = cases;
    }

    public (int, int, int) RollDice()
    {
        int index = Random.Range(0, casesGreaterThan10.Count);
        return casesGreaterThan10[index];
    }
}
