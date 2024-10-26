public class Valhein : HeroBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            maxHealth: 5000,
            speedMove: 10f,
            detectionRange: 6f,
            attackRange: 4f,
            attackDamage: 100,
            team: Team.Blue,
            HeroDatabaseAddress: "Assets/Scripts/Player/ValheinDatabase.asset",
            nameBulletHero: "BulletValhein"
        );
    }
}