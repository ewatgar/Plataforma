using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] int hp = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collect(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Collect(other);
    }

    private void Collect(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Heal(hp);
            Destroy(gameObject);
        }
    }


}
