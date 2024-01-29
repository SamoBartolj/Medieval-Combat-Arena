using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyMovementManager enemyMovementManager;
    public bool isPerforimingAction;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;

    //FOV
    public float maxDetectionAngle = 50;
    public float minDetectionAngle = -50;

    private void Awake()
    {
        enemyMovementManager = GetComponent<EnemyMovementManager>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (enemyMovementManager.currentTarget == null)
        {
            enemyMovementManager.HandleDetection();
        }
        else
        {
            enemyMovementManager.HandleMoveToTarget();
        }
    }
}
