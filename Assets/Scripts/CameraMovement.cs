using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float yPos = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = Time.deltaTime * 20;

        if (Input.GetKey("w"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed, transform.position.z);
        }
        if (Input.GetKey("s"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
        }
        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(transform.position.x - movementSpeed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
        }
    }
}