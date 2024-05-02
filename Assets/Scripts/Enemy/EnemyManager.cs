using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    EnemyMovementManager enemyMovementManager;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStats enemyStats;

    public NavMeshAgent navmeshAgent;
    public State currentState;
    public CharachterStats currentTarget;
    public Rigidbody enemyRigidBody;


    [Header("A.I. Settings")]
    public bool isPerformingAction;
    public float detectionRadius = 20;

    //FOV
    public float maxDetectionAngle = 50;
    public float minDetectionAngle = -50;

    public float currentRecoveryTime = 0;
    public float distanceFromTarget;
    public float rotationSpeed = 15f;
    public float maxAttackRange = 1.5f;
    public float viewableAngle;

    private void Awake()
    {
        enemyMovementManager = GetComponent<EnemyMovementManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        enemyRigidBody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        CalculateDistanceFromTarget();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
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

    private void CalculateDistanceFromTarget()
    {
        if(currentTarget != null)
        {
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        } 
    }


}
