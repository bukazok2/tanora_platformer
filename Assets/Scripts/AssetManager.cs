using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    [SerializeField] Bullet bullet;

    private ObjectPool<Bullet> pool;

    void Start()
    {
        Instance = this;

        this.pool = new ObjectPool<Bullet>(() =>
        {
            return Instantiate(bullet);
        },
        bullet =>
        {
            bullet.gameObject.SetActive(true);
        },
        bullet =>
        {
            bullet.gameObject.SetActive(false);
        },
        bullet =>
        {
            Destroy(bullet.gameObject);
        },
        false,
        10,
        100
        );
    }

    public Bullet GetBullet()
    {
        return this.pool.Get();
    }

    public void Release(Bullet b)
    {
        this.pool.Release(b);
    }
}
