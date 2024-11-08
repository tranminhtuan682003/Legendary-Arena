using System.Collections.Generic;
using UnityEngine;

public class XiuDiceRollStrategy : IDiceRollStrategy
{
    private List<(int, int, int)> casesLessThanOrEqual10;

    public XiuDiceRollStrategy(List<(int, int, int)> cases)
    {
        casesLessThanOrEqual10 = cases;
    }

    public (int, int, int) RollDice()
    {
        int index = Random.Range(0, casesLessThanOrEqual10.Count);
        return casesLessThanOrEqual10[index];
    }
}
