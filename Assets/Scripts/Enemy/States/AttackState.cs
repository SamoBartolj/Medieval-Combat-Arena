using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public CombatStanceState combatStanceState;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {

        Vector3 targetDierciton = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDierciton, transform.forward);

        if (enemyManager.isPerformingAction)
        {
            return combatStanceState;
        }

        if (currentAttack != null)
        {
            //If we are too close to the enemy to preform current attack, get a new attack
            if (enemyManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }

            //If we are close enough to attack, then let us proceed
            else if (enemyManager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                //If our enemy is within our attacks viewable angle, we attack
                if (enemyManager.viewableAngle <= currentAttack.maximumAttackAngle &&
                enemyManager.viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorManager.animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                        currentAttack = null;
                        return combatStanceState;
                    }
                }
            }
        }
        else
        {
            GetNewAttack(enemyManager);
        }

        return combatStanceState;
    }

    private void GetNewAttack(EnemyManager enemyManager)
    {
        if (enemyAttacks.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyAttacks.Length);
            currentAttack = enemyAttacks[randomIndex];
            Debug.Log("New attack assigned: " + currentAttack.name); 
        }
        else
        {
            Debug.Log("No valid attacks found."); 
        }
    }


}
