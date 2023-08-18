using System;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
    Attacking,
    Timeout,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    [SerializeField] EnemyScriptable enemyDetails;

    private NavMeshAgent agent;
    private GameObject enemyVisualCache;

    private EnemyState state = EnemyState.Idle;

    private Transform bulletSpawnPointCache;

    private float timeoutDuration = 2.5f;
    private float timeoutTimer = 2.5f;

    public void InitEnemy(EnemyScriptable enemyDetails)
    {
        this.enemyDetails = enemyDetails;

        this.enemyVisualCache = Instantiate(enemyDetails.enemyGameobject, this.transform);
        this.enemyVisualCache.transform.localPosition = new Vector3(0, 0.5f, 0);

        this.agent = GetComponent<NavMeshAgent>();

        this.bulletSpawnPointCache = this.enemyVisualCache.transform.Find("BulletSpawnPoint");
    }

    private void Update()
    {
        float dist = Vector3.Distance(Player.Instance.transform.position, this.transform.position);

        this.FindState(dist);

        if (this.state == EnemyState.Chase)
        {
            this.MoveToTarget();
        }
        else if (this.state == EnemyState.Attacking)
        {
            this.Attack();
        }
        else if (this.state == EnemyState.Idle)
        {
            this.Idle();
        }
        else if (this.state == EnemyState.Timeout)
        {
            this.Timeout();
        }
    }

    private void Timeout()
    {
        if (this.timeoutTimer > 0f)
        {
            this.timeoutTimer -= Time.deltaTime;
        }
        else
        {
            this.timeoutTimer = this.timeoutDuration;
            this.state = EnemyState.Idle;
        }
    }

    private void Attack()
    {
        this.agent.SetDestination(this.transform.position);
        this.RotateToPlayer();

        GameObject bulletCache = Instantiate(this.bullet, this.bulletSpawnPointCache.position, Quaternion.identity);
        Bullet b = bulletCache.GetComponent<Bullet>();
        b.BulletInit(Player.Instance.gameObject);

        this.state = EnemyState.Timeout;
    }

    private void RotateToPlayer()
    {
        Vector3 direction = Player.Instance.transform.position - this.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
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
        if (this.state == EnemyState.Timeout)
            return;

        if (distance <= this.enemyDetails.aggroRange && distance >= this.enemyDetails.attackRange)
        {
            this.state = EnemyState.Chase;
        }
        else if (distance <= this.enemyDetails.attackRange)
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

        Gizmos.DrawWireSphere(this.transform.position, 1f);

        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(this.transform.position, this.enemyDetails.attackRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.transform.position, this.enemyDetails.aggroRange);
    }
}
