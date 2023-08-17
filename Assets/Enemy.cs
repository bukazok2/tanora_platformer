using System;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
    Attacking
}

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemyDetails;

    private NavMeshAgent agent;
    private GameObject enemyVisualCache;

    private EnemyState state = EnemyState.Idle;

    private void Start()
    {

    }

    public void InitEnemy(EnemyScriptable enemyDetails)
    {
        this.enemyDetails = enemyDetails;

        this.enemyVisualCache = Instantiate(enemyDetails.enemyGameobject, this.transform);
        this.enemyVisualCache.transform.localPosition = new Vector3(0, 0.5f, 0);

        this.agent = GetComponent<NavMeshAgent>();

       
    }

    private void Update()
    {
        float dist = Vector3.Distance(Player.Instance.transform.position, this.transform.position);

        this.FindState(dist);

        if(this.state == EnemyState.Chase)
        {
            this.MoveToTarget();
        }
        else if(this.state == EnemyState.Attacking)
        {
            // attack
        }
        else if(this.state == EnemyState.Idle)
        {
            this.Idle();
        }
    }

    private void Idle()
    {
        this.agent.SetDestination(this.transform.position);
        this.agent.enabled = false;
    }

    private void MoveToTarget()
    {
        this.agent.enabled = true;
        this.agent.SetDestination(Player.Instance.transform.position);
    }

    private void FindState(float distance)
    {
        if(distance <= this.enemyDetails.aggroRange && distance >= this.enemyDetails.attackRange)
        {
            this.state = EnemyState.Chase;
        }
        else if(distance <= this.enemyDetails.attackRange)
        {
            this.state = EnemyState.Attacking;
        }
        else
        {
            this.state = EnemyState.Idle;
        }
    }

    private void OnDrawGizmos()
    {
        if (this.state == EnemyState.Chase)
        {
            Gizmos.color = Color.green;
        }
        else if (this.state == EnemyState.Attacking)
        {
            Gizmos.color = Color.red;
        }
        else if (this.state == EnemyState.Idle)
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawWireSphere(this.transform.position,1f);

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(this.transform.position,this.enemyDetails.attackRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.transform.position, this.enemyDetails.aggroRange);
    }
}
