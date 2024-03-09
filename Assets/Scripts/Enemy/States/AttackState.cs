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
        // Select one of out many attacks based on attack scores
        // if the selected attack is not being able to used because of bad angle or distance, select new attack
        //if the attack is viable, stop our movement and attack our target
        //set our recovery timer to the attacks recovery time
        //return the combat stance state

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
        Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
        enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        int maxscore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxscore += enemyAttackAction.attackScore;
                }

            }
        }

        int randomvalue = Random.Range(0, maxscore);
        int tempscore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null)
                    {
                        return;
                    }

                    tempscore += enemyAttackAction.attackScore;

                    if (tempscore > randomvalue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }

            }
        }

    }

}
