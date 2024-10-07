using UnityEngine;
using System.Collections;

public class Skill1 : Ability
{
    private PlayerController playerController;

    public Skill1(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "Rapid";
        cooldown = 5f;
        manaCost = 30f;
    }

    protected override void UseAbility()
    {
        playerController.StartCoroutine(IncreaseDistance());
    }
    IEnumerator IncreaseDistance()
    {
        playerController.SetMaxRange(3.5f);
        playerController.AdjustSpeedShoot(playerController.heroEffects.timeShoot * 0.7f);
        playerController.ActivateLightEffect();

        yield return new WaitForSeconds(4f);

        playerController.SetMaxRange(2.5f);
        playerController.AdjustSpeedShoot(playerController.heroEffects.timeShoot / 0.7f);
        playerController.DeactivateLightEffect();
    }
}
