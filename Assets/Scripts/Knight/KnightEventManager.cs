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


    //Health knight Manager
    public static event Action<KnightController> OnManaUpdated;
    public static void InvokeUpdateManaBar(KnightController knightController)
    {
        OnManaUpdated?.Invoke(knightController);
    }
    public static event Action<KnightController> OnHealthBarUpdated;
    public static void InvokeUpdateHealthBar(KnightController knightController)
    {
        OnHealthBarUpdated?.Invoke(knightController);
    }

    //Health enemy Manger
    public static event Action<ITeamMember> OnHealthEnemyUpdate;
    public static void InvokeUpdateHealthEnemy(ITeamMember enemy)
    {
        OnHealthEnemyUpdate?.Invoke(enemy);
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


    // Detect enemy of team red top
    public static event Action<GameObject, Team> OnTowerRedDetecEnemy;
    public static void InvokeTeamRedUpdateEnemy(GameObject enemy, Team team)
    {
        OnTowerRedDetecEnemy?.Invoke(enemy, team);
    }

    #region ResultGameManager
    public static event Action<bool> OnGameWin;
    public static void InvokeGameWin(bool win)
    {
        OnGameWin?.Invoke(win);
    }

    public static event Action<bool> OnGameLose;
    public static void InvokeGameLose(bool lose)
    {
        OnGameLose?.Invoke(lose);
    }

    #endregion
}
