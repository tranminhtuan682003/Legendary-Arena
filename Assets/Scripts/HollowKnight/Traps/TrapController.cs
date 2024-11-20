using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private int damage;
    private void Awake()
    {
        InitLize();
    }

    private void InitLize()
    {
        damage = 100;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<KnightController>();
        if (other.CompareTag("Knight"))
        {
            player.TakeDamage(damage);
        }
    }
}
