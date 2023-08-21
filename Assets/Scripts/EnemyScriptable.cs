using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy1", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public GameObject enemyGameobject;
    [SerializeField] public float speed;
    [SerializeField] public float dmg;
    [SerializeField] public float aggroRange = 15f;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public float maxHp = 5;
}
