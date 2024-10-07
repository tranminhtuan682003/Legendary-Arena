using UnityEngine;
using System.Collections;
public class Pushing : Ability
{
    private PlayerController playerController;
    public Pushing(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "";
        cooldown = 0f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        if (!playerController.isAttacking)
        {
            playerController.ChangeState(new PlayerAttackState(playerController));
        }
    }
}
