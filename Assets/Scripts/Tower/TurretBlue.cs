public class TurretBlue : TurretBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            maxHealth: 200,
            detectionRange: 6f,
            attackRange: 5f,
            attackDamage: 10,
            team: Team.Blue
        );
    }
}
