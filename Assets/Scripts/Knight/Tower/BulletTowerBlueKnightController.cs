using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTowerBlueKnightController : BulletTowerKnightBase
{
    protected override bool ShouldDamage(ITeamMember targetTeamMember)
    {
        return targetTeamMember.GetTeam() == Team.Red;
    }

    protected override void OnBulletHit()
    {
        base.OnBulletHit();
    }
}
