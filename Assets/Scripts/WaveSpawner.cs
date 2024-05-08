using System;
using System.IO;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    public int enemiesKilled = 0;

    public TextMeshProUGUI enemiesKilledText;

    private void Start()
    {
        SpawnEnemy();
        enemiesKilledText.text = 0.ToString();

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
        enemiesKilled += 1;
        enemiesKilledText.text = enemiesKilled.ToString();
    }

}
