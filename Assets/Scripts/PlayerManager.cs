using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerMovment playerMovment;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerMovment = GetComponent<PlayerMovment>();  
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }


    private void FixedUpdate()
    {
        playerMovment.HandleAllMovment();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
