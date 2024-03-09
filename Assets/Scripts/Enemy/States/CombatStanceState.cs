using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public PursueTargetState pursueTargetState;


    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        // potentail circle player or walk around them

        // Check for attack range
        if (enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maxAttackRange)
        {
            return attackState;
        }
        else if (enemyManager.distanceFromTarget > enemyManager.maxAttackRange)
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }


    }
}
