using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;
    [SerializeField] GameObject jumpEffect;

    void Start()
    {
        Instance = this;

        Player.OnPlayerJump += Player_OnPlayerJump;
    }

    private void Player_OnPlayerJump(Player obj)
    {
        GameObject go = Instantiate(jumpEffect, obj.transform.position, Quaternion.identity);
        Destroy(go, 2f);
    }
}
