using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class HeroParameter
{
    [Header("Component")]
    public Rigidbody rigidbody;
    public Animator animator;
    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    [Header("Move")]
    public float speed;
    public Vector2 moveDirection;
    public Vector3 movementVector;
    public bool isMoving;
    [Header("Attack")]
    public float maxAttackRange;
    public float attackRange;
    public float attackSpeed;
    public float timeAttackAnimation;
    public bool isAttacking;

    [Header("DetectTarget")]
    public List<GameObject> enemy;
    public List<GameObject> tower;
    [Header("Prefab and Transform")]
    public Dictionary<string, ParticleSystem> skillEffect;
    public GameObject supplymentary;
    public Transform spawnPoint;
    [Header("bool")]
    public bool used;
}
