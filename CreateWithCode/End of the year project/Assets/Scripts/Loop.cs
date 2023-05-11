using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public float speed = 5.0f;
    public float xBound = 90f;
    public float returnPoint = 70f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //cloudRb.AddForce(Vector3.left * speed);
        transform.position += transform.right * -speed * Time.deltaTime;

        if (transform.position.x <= -xBound){
            transform.position = new Vector3(returnPoint, transform.position.y, transform.position.z);
        }
    }
}
