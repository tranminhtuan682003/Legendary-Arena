using UnityEngine;
using System.Collections;

public class GoHome : Ability
{
    private PlayerController playerController;
    private Coroutine returnCoroutine; // Lưu lại coroutine để hủy nếu cần

    public GoHome(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "Return";
        cooldown = 0f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        // Nếu không đang return và không di chuyển, bắt đầu return
        if (returnCoroutine == null && !playerController.isMoving)
        {
            returnCoroutine = playerController.StartCoroutine(ReturnHome());
        }
    }

    IEnumerator ReturnHome()
    {
        float duration = 5f;

        // Gọi hàm ActivateEffect từ PlayerController để kích hoạt hiệu ứng returnHomeEffect
        playerController.ActivateEffect("returnHomeEffect", playerController.transform, duration);

        while (duration > 0)
        {
            if (playerController.isMoving || playerController.isAttacking)
            {
                CancelReturnHome();
                yield break;
            }

            duration -= Time.deltaTime;
            yield return null;
        }
        CancelReturnHome();
        playerController.transform.position = playerController.startPosition.position;
        Debug.Log("Player returned to start position");

        returnCoroutine = null;
    }

    private void CancelReturnHome()
    {
        if (returnCoroutine != null)
        {
            playerController.StopCoroutine(returnCoroutine);
            returnCoroutine = null;
            if (playerController.skillEffect.ContainsKey("returnHomeEffect"))
            {
                var returnHomeEffect = playerController.skillEffect["returnHomeEffect"];
                returnHomeEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }


}
