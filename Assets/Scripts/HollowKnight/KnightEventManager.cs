using System;
using UnityEngine;
public static class KnightEventManager
{
    public static Action<string> OnButtonPressed;
    public static void NotifyButtonPressed(string action)
    {
        OnButtonPressed?.Invoke(action);
    }

    public static event Action<Transform> OnKnightEnable;

    public static void TriggerKnightEnable(Transform knightTransform)
    {
        OnKnightEnable?.Invoke(knightTransform);
    }


    //Health Manager
    public static event Action<KnightController> OnHealthBarSet;
    public static void InvokeSetHealthBar(KnightController knightController)
    {
        OnHealthBarSet?.Invoke(knightController);
    }
    public static event Action<KnightController> OnHealthBarUpdated;
    public static void InvokeUpdateHealthBar(KnightController knightController)
    {
        OnHealthBarUpdated?.Invoke(knightController);
    }

    //Moves Manager
    public static event Action<TypeMove> OnMoveActived;
    public static void InvokeUpdateMove(TypeMove typeMove)
    {
        OnMoveActived?.Invoke(typeMove);
    }

    //Skills Manager
    public static event Action<TypeSkill, float, float> OnSkillActived;
    public static void InvokeUpdateSkill(TypeSkill typeSkill, float cooldown, float executionTime)
    {
        OnSkillActived?.Invoke(typeSkill, cooldown, executionTime);
    }
}
