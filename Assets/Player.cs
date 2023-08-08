using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private void FixedUpdate() // 30 frame
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * 10 * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -50 * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, 50 * Time.deltaTime);

    }

    void Update() // jelenlegi fps-en fut
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("H");
    }
}
