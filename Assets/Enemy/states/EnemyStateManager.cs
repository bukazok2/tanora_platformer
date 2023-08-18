using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;

    public EnemyChaseState chaseState = new();
    public EnemyIdleState idleState = new();
    public EnemyAttackingState attackingState = new();

    [SerializeField] public GameObject enemy;
    [SerializeField] public EnemyScriptable enemyDetails;

    // Start is called before the first frame update
    void Start()
    {
        chaseState.enemy = idleState.enemy = attackingState.enemy = enemy;
        chaseState.enemyDetails = idleState.enemyDetails = attackingState.enemyDetails = enemyDetails;
        chaseState.agent = idleState.agent = attackingState.agent = GetComponent<NavMeshAgent>();
        currentState = idleState;
        Debug.Log("manager started");
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        Debug.Log("state switched " + state.ToString());
    }
}
