using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemy;

    private void Start()
    {
        GameObject go = Instantiate(enemy.go, this.transform);
        go.transform.position = new Vector3(0, 2, 0);
    }
}
