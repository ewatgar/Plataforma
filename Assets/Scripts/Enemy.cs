using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerHealth playerHealth;
    Rigidbody2D playerRigidbody;
    [SerializeField] int damage = 1;
    [SerializeField] float damageImpulse = 2;

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
            playerHealth = other.GetComponentInParent<PlayerHealth>();
            playerRigidbody = other.GetComponentInParent<Rigidbody2D>();

            playerHealth.TakeDamage(damage);
            DamageImpulse(other);
        }
    }

    private void DamageImpulse(Collider2D other)
    {
        float playerPos = other.transform.position.x;
        float enemyPos = transform.position.x;
        float impulseDir = Mathf.Sign(playerPos - enemyPos);
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(Vector2.right * impulseDir * damageImpulse, ForceMode2D.Impulse);
    }
}
