using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody objectRb;

    public GameObject explosionEffect;
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
     private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")){
            Instantiate(explosionEffect, objectRb.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
