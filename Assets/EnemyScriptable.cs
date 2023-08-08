using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
public class EnemyScriptable : ScriptableObject
{
    [SerializeField] public GameObject go;
}
