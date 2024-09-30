using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IhealthPlayer
{
    public SphereCollider attackCollider;
    private IState currentState;
    private Rigidbody rb;

    [HideInInspector]
    public Vector2 moveDirection; // Hướng di chuyển
    private Vector3 movementVector; // Vector di chuyển trong không gian 3D

    [Header("IHealth")]
    private float maxHealth = 5000f;
    private float currentHealth;
    public bool isDead;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    #region IHealthPlayer Implementation
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(0, value);
    }

    public bool IsDead => isDead;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isDead = false;
    }

    void Start()
    {
        ChangeState(new PlayerIdleState(this)); // Bắt đầu với trạng thái Idle
    }

    void Update()
    {
        currentState?.Execute(); // Cập nhật logic theo từng trạng thái
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit(); // Thoát trạng thái cũ
        currentState = newState; // Gán trạng thái mới
        currentState.Enter(); // Vào trạng thái mới
    }

    // Phương thức nhận input từ Input System
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized; // Nhận input di chuyển và chuẩn hóa hướng di chuyển
        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y); // Chuyển đổi thành vector 3D
    }

    // Phương thức di chuyển nhân vật
    public void Move()
    {
        // Nếu có input di chuyển và nhân vật chưa chết, thì thực hiện di chuyển
        if (movementVector != Vector3.zero && !isDead)
        {
            Vector3 move = movementVector * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + move);

            // Quay nhân vật theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
        }
    }

    public void Heal(float amount)
    {
        // Chưa triển khai
    }

    public void TakeDamage(float amount)
    {
        // Chưa triển khai
    }

    public void Dead()
    {
        // Chưa triển khai
    }
}
