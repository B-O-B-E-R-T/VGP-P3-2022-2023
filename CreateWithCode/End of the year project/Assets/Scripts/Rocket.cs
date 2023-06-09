using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody objectRb;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        
        objectRb.AddForce(Vector3.right * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.x >= 40 || transform.position.x <= -40){
            Destroy(gameObject);
        }
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")){
            Instantiate(hitEffect, objectRb.position, transform.rotation);
        }
    }
}
