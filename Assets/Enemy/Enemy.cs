using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemyDetails;

    private GameObject enemyVisualCache;


    private void Start()
    {

    }

    public void InitEnemy(EnemyScriptable enemyDetails)
    {
        this.enemyVisualCache = Instantiate(enemyDetails.enemyGameobject, this.transform);
        this.enemyVisualCache.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
}
