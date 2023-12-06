using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    public AnimatorManager animatorManager;
    public InputManager inputManager;
    public Rigidbody playerRigidBody;
    public Transform cameraObject;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.1f;
    public float rayCastRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private Vector3 moveDirection;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void Update()
    {
        HandleAllMovement();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    public void HandleMovement()
    {
        if (isJumping)

        {
            moveDirection = new Vector3(0, playerRigidBody.velocity.y, 0);
        }
        else
        {
            moveDirection = (cameraObject.forward * inputManager.verticalInput) +
                            (cameraObject.right * inputManager.horizontalInput);
            moveDirection.y = 0;
            moveDirection.Normalize();
            moveDirection *= movementSpeed;
        }

        playerRigidBody.velocity = moveDirection;

            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        Vector3 movmentVelocity = moveDirection;
        playerRigidBody.velocity = movmentVelocity;
>>>>>>> parent of 9758a64 (Added Health bar and option to take damage)
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = (cameraObject.forward * inputManager.verticalInput) +
                                  (cameraObject.right * inputManager.horizontalInput);
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);

                Vector3 landingPosition = hit.point + Vector3.up * rayCastHeightOffSet;
                transform.position = landingPosition;

                if (Mathf.Abs(landingPosition.y - transform.position.y) > 0.2f)
                {
                    landingPosition.y = transform.position.y;
                }

                transform.position = landingPosition;

                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);

            }

            //Vector3 rayCastHitPoints = hit.point;
            //targetPosition.y = rayCastHitPoints.y;
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

    private void HandleInAirMovement()
    {
        if (!playerManager.isInteracting)
        {
            animatorManager.PlayTargetAnimation("Falling", true);
        }

        inAirTimer += Time.deltaTime;
        playerRigidBody.AddForce(transform.forward * leapingVelocity);
        playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
    }

    private void HandleGrounded(RaycastHit hit)
    {
        if (!isGrounded && !playerManager.isInteracting)
        {
            animatorManager.PlayTargetAnimation("Landing", true);

            Vector3 landingPosition = hit.point + Vector3.up * rayCastHeightOffSet;
            landingPosition.y = Mathf.Max(landingPosition.y, transform.position.y);

            transform.position = landingPosition;
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);
        }

        isGrounded = true;
        inAirTimer = 0;
    }

    private void HandleGroundedMovement()
    {
        if (playerManager.isInteracting || inputManager.moveAmount > 0)
        {
            transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), Time.deltaTime / 0.1f);
        }
        else
        {
            transform.position = GetTargetPosition();
        }
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = 0;
        return targetPosition;
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            playerRigidBody.velocity = new Vector3(moveDirection.x, jumpingVelocity, moveDirection.z);
        }
    }

    public void HandleDodge()
    {
        if (playerManager.isInteracting)
            return;

        animatorManager.PlayTargetAnimation("Dodge", true, true);
        // TOGGLE INVULNERABLE BOOL FOR NO HP DAMAGE DURING ANIMATION
    }
}
