using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTowerBlueKnightController : BulletTowerKnightBase
{
    protected override bool ShouldDamage(ITeamMember targetTeamMember)
    {
        // Logic kiểm tra xem đạn Blue Knight có gây sát thương không
        // Ví dụ: Chỉ gây sát thương cho team đỏ
        return targetTeamMember.GetTeam() == Team.Red;
    }

    protected override void OnBulletHit()
    {
        base.OnBulletHit();
        // Thêm hiệu ứng đặc biệt khi đạn Blue Knight va chạm
    }
}
