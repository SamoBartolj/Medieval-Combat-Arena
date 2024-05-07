using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject healthBar;
    public WaveSpawner waveSpawner;

    public bool isDead = false;

    private string saveFileName = "enemiesKilled.txt";

    private void Update()
    {
        ShowDeathScreen();
    }


    public void SaveTopScore()
    {
        int oldEnemysKilled = 0;

        if (File.Exists(saveFileName))
        {
            using (StreamReader reader = new StreamReader(saveFileName))
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    if (int.TryParse(line, out int intValue)) 
                    {
                        oldEnemysKilled = intValue;
                        Debug.Log(oldEnemysKilled);                    
                    }
                    else
                    {
                        Debug.LogWarning("Failed to parse integer from file: " + line);
                    }
                }
                else
                {
                    Debug.LogWarning("File is empty.");
                }
            }
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(saveFileName)){ writer.WriteLine("0"); }
        }

        if(waveSpawner.enemiesKilled > oldEnemysKilled)
        {
            using (StreamWriter writer = new StreamWriter(saveFileName))
            {
                writer.WriteLine(waveSpawner.enemiesKilled.ToString());
            }
        }
        
    }


    private void ShowDeathScreen()
    {
        if (isDead)
        {
            healthBar.SetActive(false);
            deathScreen.SetActive(true);
            SaveTopScore();

        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
