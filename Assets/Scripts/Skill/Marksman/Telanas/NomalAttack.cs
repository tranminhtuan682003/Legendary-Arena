
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class NomalAttack : Ability
{
    private PlayerController playerController;
    public NomalAttack(PlayerController playerController)
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
        if (playerController.heroEffects.rangeAttack == 2.5f)
        {
            playerController.StartCoroutine(ShowAttackArea());
        }
    }

    IEnumerator ShowAttackArea()
    {
        playerController.ActivateLightEffect();
        yield return new WaitForSeconds(0.5f);
        playerController.DeactivateLightEffect();
    }
}
