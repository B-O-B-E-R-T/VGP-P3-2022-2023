using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        objectRb.AddForce(Vector3.right * speed * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
