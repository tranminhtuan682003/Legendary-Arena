using UnityEngine;

public class ButtonControlManager : MonoBehaviour
{
    public TypeMove currentMove = TypeMove.None;
    public TypeSkill currentSkill = TypeSkill.None;
    public float currentCooldown = 0;

    public void OnButtonMovePressed(TypeMove move)
    {
        currentMove = move;
    }

    public void OnButtonMoveReleased()
    {
        currentMove = TypeMove.None;
    }

    public void OnButtonAttackPressed(TypeSkill skill, float cooldown)
    {
        if (currentSkill == skill) return;
        currentSkill = skill;
        currentCooldown = cooldown;
    }


    public void OnButtonAttackReleased()
    {
        currentSkill = TypeSkill.None;
    }
}
