using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Sebesség")]
    [Range(5,17)]
    [SerializeField] int speed = 10;
    [Header("Forgási sebesség")]
    [SerializeField] int rotationSpeed = 50;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player létrejött" + this.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Vector3 (1,0,0)
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(-Vector3.forward * this.speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy lefutott");
    }

    public override string ToString()
    {
        string retStr = this.transform.position.ToString();

        return retStr + base.ToString();
    }
}
