using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public EndOfGame endOfGame;
    public EnemyStats enemyStats;
    public CharachterStats charachterStats;

    public int baseHealth = 100; 
    public int healthIncrement = 10;
    public int waveInt;

    private void Start()
    {
        SpawnEnemy();
        waveInt = 1;
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned.");
            return;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        newEnemy.SetActive(true);

    }

    public void OnEnemyDeath()
    {
        SpawnEnemy();
    }
}
