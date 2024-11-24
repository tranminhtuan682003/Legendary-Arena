using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRedController : MonoBehaviour, ITeamMember
{
    private int maxHealth;
    private int currentHealth;
    private Team team;

    private void Awake()
    {
        InitLize();
    }

    private void InitLize()
    {
        team = Team.Red;
        maxHealth = 500;
        currentHealth = maxHealth;
    }

    public Team GetTeam()
    {
        return team;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }
}
