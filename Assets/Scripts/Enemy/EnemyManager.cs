using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyMovementManager enemyMovementManager;
    EnemyAnimatorManager enemyAnimatorManager;
    public bool isPerformingAction;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //FOV
    public float maxDetectionAngle = 50;
    public float minDetectionAngle = -50;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyMovementManager = GetComponent<EnemyMovementManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if(enemyMovementManager.currentTarget != null)
        {
            enemyMovementManager.distanceFromTarget = Vector3.Distance(enemyMovementManager.currentTarget.transform.position, transform.position);
        }

        if (enemyMovementManager.currentTarget == null)
        {
            enemyMovementManager.HandleDetection();
        }
        else if (enemyMovementManager.distanceFromTarget > enemyMovementManager.stoppingDistance)
        {
            enemyMovementManager.HandleMoveToTarget();
        }
        else if(enemyMovementManager.distanceFromTarget < enemyMovementManager.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }  
    }

    private void AttackTarget()
    {
        if(isPerformingAction)
        {
            return;
        }

        if(currentAttack == null)
        {
            GetNewAttack();
        }
        else
        {
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    private void GetNewAttack()
    {
        Vector3 targetsDirection = enemyMovementManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
        enemyMovementManager.distanceFromTarget = Vector3.Distance(enemyMovementManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for(int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(enemyMovementManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack 
                && enemyMovementManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
                    
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovementManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyMovementManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if(currentAttack != null)
                    {
                        return;
                    }

                    tempScore += enemyAttackAction.attackScore;

                    if(tempScore > randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }

            }
        }
    }
}
