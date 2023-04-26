using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 30.0f;
    private float zBound = 20;
    private bool speedBoost = false;
    private float speedMultiplier;

    public int speedBoostDuration = 1;

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

        playerRb.AddForce(Vector3.forward * speed * speedMultiplier * verticalInput);
        playerRb.AddForce(Vector3.right * speed * speedMultiplier * horizontalInput);

        SpeedBoost();
    }
    //Prevent the player from leaving from all sides of the screen
    void CheckBoundaries()
    {
        if (transform.position.z > zBound){
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        /*
        if (transform.position.z < -zBound){
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        */

        
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy")){
            Debug.Log("Player has collided with an enemy");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
           
        }
    }
    private void SpeedBoost(){
        if (Input.GetKeyDown(KeyCode.Space)){
            speedBoost = true;
            StartCoroutine(SpeedBoostCooldown());
        }
        if (speedBoost == true){
            speedMultiplier = 20;
        } else{ speedMultiplier = 1; }

    }
    IEnumerator SpeedBoostCooldown(){
        yield return new WaitForSeconds(0.01f);
        speedBoost = false;
    }
}
