using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Button play;

    void Start()
    {
        play.onClick.AddListener(Play);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
