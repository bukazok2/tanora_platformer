using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position, 0.1f);

        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(transform.position + Vector3.up * 0.2f, "SpawnPoint", style);
#endif
    }
}
