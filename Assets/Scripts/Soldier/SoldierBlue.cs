using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBlue : SoldierBase
{
    protected override void Start()
    {
        base.Start();

        List<Vector3> roadMaps = new List<Vector3>
        {
            new Vector3(-19, 0, -19),
            new Vector3(-15, 0, -12),
            new Vector3(-8, 0, -7),
            new Vector3(3, 0, 5),
            new Vector3(10, 0, 11),
            new Vector3(14, 0, 18),
            new Vector3(19, 0, 21)
        };

        Initialize(
            maxHealth: 500,
            speedMove: 0.75f,
            detectionRange: 5f,
            attackRange: 4f,
            attackDamage: 100,
            roadMaps: roadMaps,
            team: Team.Blue
        );
    }
}
