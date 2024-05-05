using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharachterStats
{
    public HealthBar healthBar;
    public EndOfGame endOfGame;

    PlayerAnimatorManager playerAnimatorManager;
    PlayerMovement playerMovement;

    

    private void Awake()
    {
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!playerMovement.isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            playerAnimatorManager.PlayTargetAnimation("TakeDamage01", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                playerAnimatorManager.PlayTargetAnimation("Death01", true);
                Destroy(gameObject, 2);

                if (endOfGame != null)
                {
                    endOfGame.isDead = true;
                }
                else
                {
                    Console.WriteLine("endOfGame is null");
                }
            }
        }



    }
}
