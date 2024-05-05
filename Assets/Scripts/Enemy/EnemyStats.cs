using UnityEngine;

public class EnemyStats : CharachterStats
{
    private Animator animator;
    private WaveSpawner waveSpawner;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        waveSpawner = FindObjectOfType<WaveSpawner>(); 
    }

    private void Start()
    {
        InitializeHealth();
    }

    private void InitializeHealth()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        return healthLevel * 10;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayDamageAnimation();
        }
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.Play("Death01");
        }

        Destroy(gameObject, 2);

        if (waveSpawner != null)
        {
            waveSpawner.OnEnemyDeath();
        }

        
    }

    private void PlayDamageAnimation()
    {
        if (animator != null)
        {
            animator.Play("TakeDamage01");
        }
    }
}
