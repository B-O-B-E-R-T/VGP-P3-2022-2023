using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLoop : MonoBehaviour
{
    public float speed = 5.0f;
    private float xBound = 90f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //cloudRb.AddForce(Vector3.left * speed);
        transform.position += transform.right * -speed * Time.deltaTime;

        if (transform.position.x < -xBound){
            transform.position = new Vector3(70, transform.position.y, transform.position.z);
        }
    }
}
