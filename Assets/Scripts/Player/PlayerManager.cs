using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerMovement playerMovement;
    public PauseManager pauseManager;

    public bool isInteracting;
    public bool isUsingRootMotion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerMovement = GetComponent<PlayerMovement>();  
    }

    private void Update()
    {
        if (!pauseManager.isPaused)
        {
            inputManager.HandleAllInputs();
        }
        
    }


    private void FixedUpdate()
    {
        playerMovement.HandleAllMovment();
    }

    private void LateUpdate()
    {
        inputManager.heavyAttack_input = false;
        inputManager.lightAttack_input = false;

        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");

        playerMovement.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovement.isGrounded);

        playerMovement.isInvincible = animator.GetBool("isInvincible");
        animator.SetBool("isInvincible", playerMovement.isInvincible);
    }
}
