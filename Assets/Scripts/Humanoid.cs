using System;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour, ITakeDamage
{
    [SerializeField] protected GameObject bullet;
    public static event Action<Humanoid> OnHumanoidDie = delegate { };

    protected float hp;
    protected Transform bulletSpawnPoint;
    protected GameObject target;

    protected void RotateToTarget()
    {
        Vector3 direction = this.target.transform.position - this.transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        this.transform.rotation = targetRotation;
    }

    public void TakeDamage(float damage)
    {
        this.hp -= damage;
        if (this.hp <= 0)
        {
            this.Die();
        }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
        OnHumanoidDie?.Invoke(this);
    }

    protected virtual void Attack()
    {
        if (this.target == null)
            return;

        this.RotateToTarget();
        GameObject bulletCache = Instantiate(this.bullet, this.bulletSpawnPoint.position, Quaternion.identity);
        Bullet b = bulletCache.GetComponent<Bullet>();
        b.BulletInit(this.target);
    }
}
