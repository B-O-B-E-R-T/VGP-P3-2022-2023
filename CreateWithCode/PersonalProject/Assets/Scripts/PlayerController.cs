using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 10.0f;
    private float zBound = 11;
    private float zNewPos = 10f;
    private float xBound = 25;
    private float xNewPos = 24;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CheckBoundaries();
    }
    //Move the player by arrow key input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }
    //Prevent the player from leaving from all sides of the screen
    void CheckBoundaries()
    {
        if (transform.position.z > zBound){
            transform.position = new Vector3(transform.position.x, transform.position.y, -zNewPos);
        }
        if (transform.position.z < -zBound){
            transform.position = new Vector3(transform.position.x, transform.position.y, zNewPos);
        }

        if (transform.position.x > xBound){
            transform.position = new Vector3(-xNewPos, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBound){
            transform.position = new Vector3(xNewPos, transform.position.y, transform.position.z);
        }
    }
}
