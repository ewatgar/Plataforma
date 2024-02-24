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
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Sprite[] healthBarSprites;

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

    public void Heal(int hp)
    {
        Health += hp;
    }

    public void TakeDamage(int damage)
    {
        if (Health > 1)
        {
            Health -= damage;
            animator.Play("PlayerDamage");
            animator.Play("PlayerIdle");
        }
        else if (Health == 1)
        {
            Health -= damage;
            Dead();
        }
        Debug.Log(Health);
    }

    public void Dead()
    {
        StartCoroutine(Dying(true));
    }

    public IEnumerator Dying(bool playAnimation)
    {
        playerMovement.canMove = false;
        if (playAnimation) animator.Play("PlayerDie");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Health = maxHealth;
        yield return new WaitForSeconds(0.5f);
        playerMovement.canMove = true;
        animator.Play("PlayerIdle");
    }
}
