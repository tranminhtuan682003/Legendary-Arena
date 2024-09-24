using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Rigidbody rb;
    public Animator animator;
    protected float moveSpeed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
}
