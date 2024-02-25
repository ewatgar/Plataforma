using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator animator;
    int maxHealth = 3;
    [SerializeField] private int initHealth = 3;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Sprite[] healthBarSprites;

    public int Health
    {
        get => initHealth;
        set => initHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        Health = initHealth;
        ChangeHealthBarValue(Health);
    }

    public void Heal(int hp)
    {
        Health += hp;
        ChangeHealthBarValue(Health);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health > 0)
        {
            StartCoroutine(DamageAnimation());
        }
        else
        {
            Dead();
        }
        ChangeHealthBarValue(Health);
    }

    IEnumerator DamageAnimation()
    {
        animator.Play("PlayerDamage");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.5f);
        animator.Play("PlayerIdle");
    }

    public void Dead()
    {
        StartCoroutine(Dying(true));
    }

    public IEnumerator Dying(bool playAnimation)
    {
        playerMovement.canMove = false;
        if (playAnimation) animator.Play("PlayerDie");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(0.5f);
        Health = maxHealth;
        ChangeHealthBarValue(maxHealth);
        animator.Play("PlayerIdle");
        playerMovement.canMove = true;
    }

    public void ChangeHealthBarValue(int health)
    {
        healthBar.GetComponent<Image>().sprite = healthBarSprites[health];
    }

    public void FallDeath()
    {
        ChangeHealthBarValue(0);
        StartCoroutine(Dying(false));
    }
}
