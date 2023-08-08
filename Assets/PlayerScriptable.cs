using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Players/NewPlayer", order = 1)]
public class PlayerScriptable : ScriptableObject
{
    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] public float turnSpeed = 50f;
}
