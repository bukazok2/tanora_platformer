using UnityEngine;
using DG.Tweening;
using System;

public class Bullet : MonoBehaviour
{
    public void BulletInit(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target cannot be null");
            Destroy(this.gameObject);
        }

        this.transform.DOMove(target.transform.position, 1f).OnComplete(DestroyMe);
    }

    private void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        ITakeDamage damageable = other.gameObject.GetComponent<ITakeDamage>();

        if (damageable != null)
        {
            damageable.TakeDamage(1f);
        }
    }
}
