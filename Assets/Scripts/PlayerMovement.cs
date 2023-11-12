using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    public float movemntSpeed = 7;
    public float rotationSpeed = 15;
         

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovment()
    {
        HandleMovment();
        HandleRotation();
    }

    private void HandleMovment()
    {
        moveDirection = cameraObject.forward * inputManager.vertiacalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movemntSpeed;

        Vector3 movmentVelocity = moveDirection;
        playerRigidBody.velocity = movmentVelocity;
    }


    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.vertiacalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion tartgetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, tartgetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
