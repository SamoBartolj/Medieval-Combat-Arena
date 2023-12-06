using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;

    public Vector2 movmentInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;


    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool jump_Input;
    public bool dodge_Input;

    public bool lightAttack_input;
    public bool heavyAttack_input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();

        if (playerAttacker == null)
        {
            Debug.LogError("PlayerAttacker component not found on the same GameObject as InputManager.");
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();


            playerControls.PlayerMovment.Movement.performed += i => movmentInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovment.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;
            playerControls.PlayerActions.Dodge.performed += i => dodge_Input = true;
            playerControls.PlayerActions.LightAttack.performed += i => lightAttack_input = true;
            playerControls.PlayerActions.HeavyAttack.performed += i => heavyAttack_input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovmentInput();
        HandleJumpingInput();
        HandleDodgeInput();
        HandleAttackInput();    }


    private void HandleMovmentInput()
    {
        verticalInput = movmentInput.y;
        horizontalInput = movmentInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;


        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            playerMovement.HandleJumping();
        }
    }

    private void HandleDodgeInput()
    {
        if (dodge_Input)
        {
            dodge_Input = false;
            playerMovement. HandleDodge ();
        }

    }

    private void HandleAttackInput()
    {
        if(lightAttack_input)
        {
            playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
        }

        if (heavyAttack_input)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }
}



