using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Damage(other);
    }

    private void Damage(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
