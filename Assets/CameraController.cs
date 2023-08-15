using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam1;

    /*[SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;*/

    void Awake()
    {
        Player.OnPlayerSpawned += Player_OnPlayerSpawned;
    }

    private void Player_OnPlayerSpawned(Player obj)
    {
        vcam1.LookAt = Player.Instance.transform;
        vcam1.Follow = Player.Instance.transform;
        Debug.Log("Player Spawned");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.Instance != null)
        {
            /*Vector3 targetPosition = Player.Instance.transform.position + offset;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, this.smoothTime);
            this.transform.LookAt(Player.Instance.transform);*/
        }
    }
}
