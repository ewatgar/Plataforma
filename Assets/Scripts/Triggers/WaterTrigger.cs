using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckPlayerInWater(other, true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CheckPlayerInWater(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CheckPlayerInWater(other, false);
    }

    private void CheckPlayerInWater(Collider2D other, bool waterState)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerMovement = other.GetComponentInParent<PlayerMovement>();
            playerMovement.bIsInWater = waterState;
        }
    }
}
