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
        Debug.Log("vida inicial: " + Health);
    }

    public void Heal(int hp)
    {
        Health += hp;
        ChangeHealthBarValue(Health);
        Debug.Log("player se cura, vida actual: " + Health);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health > 1)
        {
            animator.Play("PlayerDamage");
            animator.Play("PlayerIdle");
        }
        else if (Health == 1)
        {
            Dead();
        }
        ChangeHealthBarValue(Health);
        Debug.Log("player recibe daño, vida actual: " + Health);
    }

    public void Dead()
    {
        StartCoroutine(Dying(true));
    }

    public IEnumerator Dying(bool playAnimation)
    {
        playerMovement.canMove = false;
        if (playAnimation) animator.Play("PlayerDie");
        yield return new WaitForSeconds(0.3f);
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
