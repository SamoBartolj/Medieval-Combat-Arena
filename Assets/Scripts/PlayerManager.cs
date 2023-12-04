using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerMovment playerMovment;



    public bool isInteracting;
    public bool isUsingRootMotion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        inputManager.heavyAttack_input = false;
        inputManager.lightAttack_input = false;

        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");

        playerMovment.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerMovment.isGrounded); 
    }
}
