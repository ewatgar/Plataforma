using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    [SerializeField] private int _health = 3;

    public int Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, maxHealth);
    }

    private void Start()
    {
        Health = _health;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {

    }

    public void Heal(int hp)
    {
        Health += hp;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
