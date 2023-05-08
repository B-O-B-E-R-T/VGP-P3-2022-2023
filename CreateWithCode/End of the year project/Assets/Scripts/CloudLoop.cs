using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLoop : MonoBehaviour
{
    public float speed = 5.0f;
    public float xBound = -25f;
    private Rigidbody cloudRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);

            if (transform.position.x < -xBound){
                Destroy(gameObject);
            }
    }
}
