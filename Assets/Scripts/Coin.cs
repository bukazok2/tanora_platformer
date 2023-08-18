using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 1f, 0));
    }

    public void Collect()
    {
        Debug.Log("Biztos a player jött nekem");
        if(Player.Instance != null)
        {
            Player.Instance.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
