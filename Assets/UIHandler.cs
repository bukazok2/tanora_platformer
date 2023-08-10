using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Button play;

    void Start()
    {
        play.onClick.AddListener(Play);

        Global.score = 1;

        DontDestroyOnLoad(this);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
