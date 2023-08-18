using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        agent.SetDestination(Player.Instance.transform.position);
    }

    public override void OnDrawGizmos(EnemyStateManager enemy)
    {
        Gizmos.color = Color.green;
        DrawEverything();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (PlayerEnemyDistance <= enemyDetails.attackRange) 
        {
            enemy.SwitchState(enemy.attackingState);
        }
    }
}
