using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PursueTargetState pursueTargetState;
    public LayerMask detectionLayer;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        // Look for a potential target 
        // Switch to the pursue state if target is found
        // if not return this state

        #region Handle Enemy Target Detection

        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharachterStats charachterStats = colliders[i].transform.GetComponent<CharachterStats>();

            if (charachterStats != null)
            {
                enemyManager.currentTarget = charachterStats;
                Debug.Log("Target detected: " + enemyManager.currentTarget.name);
                break; 
            }
        }
        #endregion

        #region Handle Switching To Next State

        if (enemyManager.currentTarget != null)
        {
            return pursueTargetState;

        }
        else
        {
            return this;
        }

        #endregion



    }

}
