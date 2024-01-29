using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyMovementManager enemyMovementManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovementManager = GetComponentInParent<EnemyMovementManager>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyMovementManager.enemyRigidBody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyMovementManager.enemyRigidBody.velocity = velocity;
    }
}
