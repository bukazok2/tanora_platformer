using UnityEngine;
using UnityEngine.SceneManagement;

public class Barmi : MonoBehaviour
{
    [Header("Teszt")]
    [Range(0,5)]
    [SerializeField] int valtozo;

    [Header("Teszt2")]
    [Range(0, 10)]
    [SerializeField] int valtozoKetto;

    [SerializeField] GameObject player;

    private void Start()
    {
        SceneManager.LoadScene("InGameUI", LoadSceneMode.Additive);
        Instantiate(player);


    }
}
