using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharachterStats
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }


    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (animator != null)
        {
            animator.Play("TakeDamage01");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death01");

                Destroy(gameObject, 3);
            }
        }
    }
}
