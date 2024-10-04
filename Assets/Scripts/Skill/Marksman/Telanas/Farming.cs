using UnityEngine;
using System.Collections;

public class Farming : Ability
{
    private PlayerController playerController;
    public Farming(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "";
        cooldown = 0f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        Attack();
    }

    private void Attack()
    {
        playerController.ChangeAnimator("Attack");
        playerController.ActivateEffect("shootEffect", playerController.SpawnPoint, 0.5f);
        GameObject bullet = ObjectPool.Instance.GetFromPool(playerController.heroEffects.bulletPrefab, playerController.SpawnPoint.position, playerController.SpawnPoint.rotation);
        bullet.GetComponent<BulletTelAnas>().SetMaxRange(playerController.heroEffects.rangeAttack);
    }
}
