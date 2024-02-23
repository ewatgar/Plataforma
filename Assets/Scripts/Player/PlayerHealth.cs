using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    public int maxHealth = 3;
    [SerializeField] private int _health = 3;

    public int Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, maxHealth);
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        Health = _health;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        StartCoroutine(Dying());
    }

    public void Heal(int hp)
    {
        Health += hp;
    }

    public void TakeDamage(int damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            animator.Play("PlayerDamage");
            animator.Play("PlayerIdle");
        }
    }

    public IEnumerator DeadRestart()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Health = maxHealth;
        yield return new WaitForSeconds(0.5f);
        playerMovement.canMove = true;
        animator.Play("PlayerIdle");
    }

    IEnumerator Dying()
    {
        playerMovement.canMove = false;
        animator.Play("PlayerDie");
        yield return new WaitForSeconds(1);
        StartCoroutine(DeadRestart());
    }
}
