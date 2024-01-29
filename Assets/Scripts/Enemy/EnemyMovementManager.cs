using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovementManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;
    NavMeshAgent navmeshAgent;
    public Rigidbody enemyRigidBody;

    public CharachterStats currentTarget;
    public LayerMask detectionLayer;

    public float distanceFromTarget;
    public float stoppingDistance = 1f;

    public float rotationSpeed = 15f;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navmeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharachterStats charachterStats = colliders[i].transform.GetComponent<CharachterStats>();

            if (charachterStats != null)
            {
                // CHECK FOR TEAM ID

                Vector3 targetDirection = charachterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minDetectionAngle && viewableAngle < enemyManager.maxDetectionAngle)
                {
                    currentTarget = charachterStats;
                    Debug.Log("Target detected: " + currentTarget.name);
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        Debug.Log("HandleMoveToTarget called");

        if (currentTarget == null)
        {
            Debug.LogError("currentTarget is null in HandleMoveToTarget method.");
            return;
        }

        Vector3 targetDierciton = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDierciton, transform.forward);

        //If performing an action, stop movement
        if(enemyManager.isPerformingAction)
        {
            enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navmeshAgent.enabled = false;
        }

        else
        {
            if(distanceFromTarget > stoppingDistance)
            {
                if (enemyAnimatorManager != null && enemyAnimatorManager.animator != null)
                {
                    enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else
                {
                    Debug.LogError("enemyAnimatorManager or animator is null.");
                }
            }

            else if(distanceFromTarget <= stoppingDistance)
            {
                enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();

      
         navmeshAgent.transform.localPosition = Vector3.zero;
         navmeshAgent.transform.localRotation = Quaternion.identity;
       

    }

    private void HandleRotateTowardsTarget ()
    {
        if (navmeshAgent == null)
        {
            Debug.LogError("navmeshAgent is null in HandleRotateTowardsTarget method.");
            return;
        }

        //Rotate manually
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }

        //Rotate with pathfinding
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navmeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidBody.velocity;

            navmeshAgent.enabled = true;
            navmeshAgent.SetDestination(currentTarget.transform.position);
            enemyRigidBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navmeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }

}
