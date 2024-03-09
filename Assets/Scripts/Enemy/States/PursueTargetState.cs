using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;


    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    { 
        // Chase the target
        // If within attack range, switch to combat stance state
        // if target is out of range, return this state and contuinue to chase target

        #region Handle Move To Target

        if (enemyManager.currentTarget == null)
        {
            Debug.LogError("currentTarget is null in HandleMoveToTarget method.");
            return this;
        }

        Vector3 targetDierciton = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDierciton, transform.forward);

        if (enemyManager.distanceFromTarget > enemyManager.maxAttackRange)
        {
            enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }
     
        HandleRotateTowardsTarget(enemyManager);
        enemyManager.navmeshAgent.transform.localPosition = Vector3.zero;
        enemyManager.navmeshAgent.transform.localRotation = Quaternion.identity;

        #endregion

        #region Handle Switching To Next State

        if (enemyManager.distanceFromTarget <= enemyManager.maxAttackRange)
        {
            Debug.Log("Switching to Combat Stance State.");
            return combatStanceState;
        }
        else
        {
            return this;
        }

        #endregion

    }

    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.navmeshAgent == null)
        {
            Debug.LogError("navmeshAgent is null in HandleRotateTowardsTarget method.");
            return;
        }

        //Rotate manually
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed);
        }

        //Rotate with pathfinding
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

            enemyManager.navmeshAgent.enabled = true;
            enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidBody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }
}
