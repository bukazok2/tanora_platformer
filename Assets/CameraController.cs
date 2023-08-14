using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam1;
    private bool inited = false;

    /*[SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;*/

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.Instance != null)
        {
            if(!this.inited)
            {
                vcam1.LookAt = Player.Instance.transform;
                vcam1.Follow = Player.Instance.transform;
                this.inited = true;
            }
            /*Vector3 targetPosition = Player.Instance.transform.position + offset;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, this.smoothTime);
            this.transform.LookAt(Player.Instance.transform);*/
        }
    }
}
