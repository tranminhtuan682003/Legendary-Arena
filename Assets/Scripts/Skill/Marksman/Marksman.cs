using UnityEngine;
public class Marksman : Heros
{
    private PlayerController playerController;
    public Marksman(PlayerController playerController)
    {
        this.playerController = playerController;
        heroName = "Marksman";
        SetupAbilities();
    }

    public override void SetupAbilities()
    {
        abilities.Add(new NomalAttack(playerController));
        abilities.Add(new Skill1(playerController));
        abilities.Add(new Skill2());
        abilities.Add(new Skill3());
        abilities.Add(new GoHome(playerController));
        abilities.Add(new Heal(playerController));
        abilities.Add(new Explosive());
        abilities.Add(new Farming(playerController));
        abilities.Add(new Pushing(playerController));


        Debug.Log("Số lượng kỹ năng của Marksman: " + abilities.Count);
    }
}
