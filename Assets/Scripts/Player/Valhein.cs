public class Valhein : HeroBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            maxHealth: 5000,
            speedMove: 10f,
            detectionRange: 7f,
            attackRange: 4f,
            attackDamage: 1000,
            team: Team.Blue,
            HeroDatabaseAddress: "Assets/Scripts/Player/ValheinDatabase.asset",
            nameBulletHero: "BulletValhein",
            attackInterval: TimeRunAnimation("Attack")

        );
    }
}