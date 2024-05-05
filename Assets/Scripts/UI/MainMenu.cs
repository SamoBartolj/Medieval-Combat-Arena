using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Arenas[] arenas;
    [SerializeField] private PlayersSO[] players;

    public Image arenaImage;
    public TMP_Text arenaName;

    public Image playerImage;
    public TMP_Text playerName;


    public int arenaIndex;
    public int playerIndex;

    private void Awake()
    {
        arenaIndex = 0;
        playerIndex = 0;
    }


    private void Start()
    {
        SetArenaData();
        SetPlayerData();
    }


    #region Choose Arena
    public void ChooseArenaIndexAdd()
    {
        arenaIndex++;

        if (arenaIndex >= arenas.Length)
            arenaIndex = 0;

        SetArenaData();
    }

    public void ChooseArenaIndexSubtract()
    {
        arenaIndex--;

        if (arenaIndex < 0)
            arenaIndex = arenas.Length - 1; 

        SetArenaData();
    }

    private void SetArenaData()
    {
        Debug.Log(arenaIndex);

        if (arenaIndex >= 0 && arenaIndex < arenas.Length)
        {
            arenaImage.sprite = arenas[arenaIndex].arenaImage;
            arenaName.text = arenas[arenaIndex].arenaName;
        }
        else
        {
            Debug.LogError("Arena index out of bounds!" + arenaIndex + arenas.Length);
        }
    }

    #endregion

    #region

    public void ChoosePlayerIndexAdd()
    {
        playerIndex++;

        if (playerIndex >= players.Length)
            playerIndex = 0;

        SetPlayerData();
    }

    public void ChoosePlayerIndexSubtract()
    {
        playerIndex--;

        if (arenaIndex < 0)
            playerIndex = arenas.Length - 1;

        SetPlayerData();
    }


    private void SetPlayerData()
    {
        if (playerIndex >= 0 && playerIndex < players.Length)
        {
            playerImage.sprite = players[playerIndex].playerImage;
            playerName.text = players[playerIndex].playerName;
        }
        else
        {
            Debug.LogError("Player index out of bounds!" + playerIndex + arenas.Length);
        }
    }

    #endregion


    public void PlayGame()
    {
        PlayerPrefs.SetInt("PlayerIndex", playerIndex);

        SceneManager.LoadScene(arenas[arenaIndex].buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
