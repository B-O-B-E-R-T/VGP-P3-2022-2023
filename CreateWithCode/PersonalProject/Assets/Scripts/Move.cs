using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{   
    public float speed = 5.0f;
    public bool down = true;
    private float zDestroy = 30f;
    private Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        if (gameObject.CompareTag("Enemy")){
            objectRb.AddForce(Vector3.forward * -speed * 5, ForceMode.Impulse);
        } else {
            objectRb.AddForce(Vector3.forward * speed * 5, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (down){
            objectRb.AddForce(Vector3.forward * -speed);

            if (transform.position.z < -zDestroy){
                Destroy(gameObject);
            }
        } else {
            objectRb.AddForce(Vector3.forward * speed);

            if (transform.position.z > zDestroy+40){
                Destroy(gameObject);
            }
        }
    }
}
