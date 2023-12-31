using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    AnimatorManager animatorManager;

    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
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
        currentHealth -= damage; 
        healthBar.SetCurrentHealth(currentHealth);

        animatorManager.PlayTargetAnimation("TakeDamage01", true);

        if (currentHealth <= 0) 
        {
            currentHealth = 0;

            animatorManager.PlayTargetAnimation("Death01", true);
            //HANDLE PLAYER DEATH
        }
    }
}
