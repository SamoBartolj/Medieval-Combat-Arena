using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject healthBar;

    public bool isDead = false;

    private void Update()
    {
        ShowDeathScreen();
    }





    private void ShowDeathScreen()
    {
        if (isDead)
        {
            healthBar.SetActive(false);
            deathScreen.SetActive(true);

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
