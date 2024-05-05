using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerBlue;
    public GameObject playerBlack;
    public GameObject playerYellow;
    public GameObject playerOrange;

    public CameraManager cameraManager;
    int playerIndex;

    private void Awake()
    {
        playerIndex = PlayerPrefs.GetInt("PlayerIndex", 0);

        switch (playerIndex)
        {
            case 0:
                playerBlue.SetActive(true); break;

            case 1:
                playerBlack.SetActive(true); break;

            case 2:
                playerYellow.SetActive(true); break;

            case 3:
                playerOrange.SetActive(true); break;
        }
    }

}
