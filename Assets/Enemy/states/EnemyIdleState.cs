using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        agent.SetDestination(enemy.transform.position);
    }

    public override void OnDrawGizmos(EnemyStateManager enemy)
    {
        Gizmos.color = Color.blue;
        DrawEverything();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (PlayerEnemyDistance <= enemyDetails.aggroRange && PlayerEnemyDistance >= enemyDetails.attackRange)
        {
            enemy.SwitchState(enemy.chaseState);
        }
    }

}
