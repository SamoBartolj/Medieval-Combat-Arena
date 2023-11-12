using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;

    public Vector2 movmentInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;


    private float moveAmount;
    public float vertiacalInput;
    public float horizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();  
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovment.Movment.performed += i => movmentInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovment.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
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
    }

    private void HandleMovmentInput()
    {
        vertiacalInput = movmentInput.y;
        horizontalInput = movmentInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(vertiacalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }



}


