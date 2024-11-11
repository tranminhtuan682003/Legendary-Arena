using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    private Rigidbody2D rigitbody;
    private Animator animator;

    private bool isDead;
    public float jumpForce = 5f;

    private void Awake()
    {
        InitLize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            HandleMove();
        }
    }

    private void InitLize()
    {
        animator = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
    }

    private void HandleMove()
    {
        SoundFlappyManager.Instance.PlayWingBet();
        rigitbody.velocity = Vector2.zero;

        rigitbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            isDead = true;
            gameObject.SetActive(false);
            SoundFlappyManager.Instance.PlayDieSound();
            FlappyBirdEventManager.TriggerGameOver();
        }
    }

}
