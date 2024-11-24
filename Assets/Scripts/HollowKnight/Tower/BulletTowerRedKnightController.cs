using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTowerRedKnightController : BulletTowerKnightBase
{
    protected override bool ShouldDamage(ITeamMember targetTeamMember)
    {
        // Logic kiểm tra xem đạn Red Knight có gây sát thương không
        // Ví dụ: Chỉ gây sát thương cho team xanh
        return targetTeamMember.GetTeam() == Team.Blue;
    }

    protected override void OnBulletHit()
    {
        base.OnBulletHit();
        // Thêm hiệu ứng đặc biệt khi đạn Red Knight va chạm
        Debug.Log("Red Knight bullet hit!");
    }
}
