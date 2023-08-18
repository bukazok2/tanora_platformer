using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;

    void Start()
    {
        Invoke("Blend", 2f);
    }

    private void Blend()
    {
        vcam1.Priority = 0;
        vcam2.Priority = 1;
    }



}
