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
        yPos += Time.deltaTime * 3;

        transform.position = new Vector3(transform.position.x, Mathf.Cos(yPos) * 5, transform.position.z);
    }
}