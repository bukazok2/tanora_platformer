using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("enemy: attack");
    }

    public override void OnDrawGizmos(EnemyStateManager enemy)
    {
        Gizmos.color = Color.red;
        DrawEverything();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //throw new System.NotImplementedException();
    }
}
