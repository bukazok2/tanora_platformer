using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayHandler : MonoBehaviour
{
    public static GamePlayHandler Instance;

    [SerializeField] GameObject playerPrefab;

    public void Start()
    {
        if (Instance == null)
            Instance = this;

        SceneManager.LoadScene("InGameUI", LoadSceneMode.Additive);

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    public void Init()
    {
        Debug.Log("Valami");
    }

}
