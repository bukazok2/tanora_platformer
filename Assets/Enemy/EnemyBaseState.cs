using UnityEngine;
using UnityEngine.AI;

abstract public class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
    public abstract void OnDrawGizmos(EnemyStateManager enemy);

    public GameObject enemy;
    public EnemyScriptable enemyDetails;

    public NavMeshAgent agent;

    public float PlayerEnemyDistance => Vector3.Distance(Player.Instance.transform.position, enemy.transform.position);

    public void DrawEverything()
    {
        Gizmos.DrawWireSphere(enemy.transform.position, 1f);

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(enemy.transform.position, enemyDetails.attackRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(enemy.transform.position, enemyDetails.aggroRange);
    }
}
