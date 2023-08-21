using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

public class GameObjectSpawner : MonoBehaviour
{
    public static Dictionary<Enemy, GameObject> spawnedEnemies = new Dictionary<Enemy, GameObject>();

    [SerializeField] GameObject enemy;

    [SerializeField] List<EnemyScriptable> enemies;

    private void Start()
    {
        Invoke("Spawn", 1f);
        Humanoid.OnHumanoidDie += Enemy_OnEnemyDie;
    }

    private void Enemy_OnEnemyDie(Humanoid obj)
    {
        Enemy e = null;
        if (obj is Enemy)
        {
            e = (Enemy)obj;
        }

        if (spawnedEnemies.ContainsKey(e))
        {
            spawnedEnemies.Remove(e);
        }
    }

    public void Spawn()
    {
        Debug.Log("Spawn");
        foreach (EnemyScriptable item in enemies)
        {
            GameObject enemyCache = Instantiate(enemy, this.transform.position, Quaternion.identity);
            Enemy e = enemyCache.GetComponent<Enemy>();
            e.InitEnemy(item);
            spawnedEnemies.Add(e, e.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position, 0.1f);

        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(transform.position + Vector3.up * 0.2f, "EnemySpawnPoint", style);
#endif
    }
}
