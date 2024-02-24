using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    PlayerHealth playerHealth;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FallDeath(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        FallDeath(other);
    }

    private void FallDeath(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            StartCoroutine(playerHealth.Dying(false));
        }
    }
}
