using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] Button myBtn;

    private void Start()
    {
        myBtn.onClick.AddListener(Method);
    }

    public void Method()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
