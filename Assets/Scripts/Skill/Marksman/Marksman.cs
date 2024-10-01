using UnityEngine;
public class Marksman : Heros
{
    public Marksman()
    {
        heroName = "Marksman";
        SetupAbilities();
    }

    public override void SetupAbilities()
    {
        abilities.Add(new ShootArrow());
        abilities.Add(new RapidFire());
        abilities.Add(new SniperShot());
        abilities.Add(new SlowDown());

        Debug.Log("Số lượng kỹ năng của Marksman: " + abilities.Count);
    }
}
