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
        if (!playerController.isAttacking)
        {
            playerController.ChangeState(new PlayerAttackState(playerController));
        }
    }
}
