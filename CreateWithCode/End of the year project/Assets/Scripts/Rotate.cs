using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float xRotationSpeed = 0f;
    public float yRotationSpeed = 0f;
    public float zRotationSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotationSpeed*Time.deltaTime, yRotationSpeed*Time.deltaTime, zRotationSpeed*Time.deltaTime);
    }
}
