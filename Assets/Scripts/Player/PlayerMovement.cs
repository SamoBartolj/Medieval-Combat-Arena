using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    public Rigidbody playerRigidBody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.1f;
    public LayerMask groundLayer;

    [Header("Movment Flags")]
    public bool isGrounded;
    public bool isJumping;
    public bool isInvincible;

    [Header("Movment Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;  //Vrednost mora vedno biti negativna




    public void Awake()
    {

        playerManager = GetComponent<PlayerManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovment()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();

    }

    private void HandleMovement()
    {
        if (isJumping)
        {
            moveDirection = Vector3.zero;
            moveDirection.y = playerRigidBody.velocity.y;
        }
        else
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection *= movementSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion tartgetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, tartgetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                playerAnimatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                playerAnimatorManager.PlayTargetAnimation("Landing", true);

                Vector3 landingPosition = hit.point + Vector3.up * rayCastHeightOffSet;
                transform.position = landingPosition;

                if (Mathf.Abs(landingPosition.y - transform.position.y) > 0.2f)
                {
                    landingPosition.y = transform.position.y;
                }

                transform.position = landingPosition;

                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);

            }

            Vector3 rayCastHitPoints = hit.point;
            targetPosition.y = rayCastHitPoints.y;
            inAirTimer = 0;
            isGrounded = true;
        }

        else
        {
            isGrounded = false;

        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            playerAnimatorManager.animator.SetBool("isJumping", true);
            playerAnimatorManager.PlayTargetAnimation("Jumping", false);
            Debug.Log("Skace");

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
    }

    public void HandleDodge()
    {
        if (playerManager.isInteracting)
            return;


        playerAnimatorManager.animator.SetBool("isInvincible", true);
        playerAnimatorManager.PlayTargetAnimation("Dodge", true, true, true);
    }
}
