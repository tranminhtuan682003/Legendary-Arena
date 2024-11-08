using System.Collections.Generic;
using UnityEngine;

public class DiceCaseGenerator : MonoBehaviour
{
    public List<(int, int, int)> GenerateCasesGreaterThan10()
    {
        List<(int, int, int)> cases = new List<(int, int, int)>();

        for (int x = 1; x <= 6; x++)
        {
            for (int y = 1; y <= 6; y++)
            {
                for (int z = 1; z <= 6; z++)
                {
                    if (x + y + z > 10)
                    {
                        cases.Add((x, y, z));
                    }
                }
            }
        }
        return cases;
    }

    public List<(int, int, int)> GenerateCasesLessThanOrEqual10()
    {
        List<(int, int, int)> cases = new List<(int, int, int)>();

        for (int x = 1; x <= 6; x++)
        {
            for (int y = 1; y <= 6; y++)
            {
                for (int z = 1; z <= 6; z++)
                {
                    if (x + y + z <= 10)
                    {
                        cases.Add((x, y, z));
                    }
                }
            }
        }
        return cases;
    }
}
