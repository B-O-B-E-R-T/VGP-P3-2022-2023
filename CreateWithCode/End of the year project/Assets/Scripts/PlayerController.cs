using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 30.0f;
    private float bound = 18;
    private float rocketCooldown = 1f;
    private bool canShoot = true;

    public GameObject rocket;
    public GameObject explosionEffect;

    private GameManager gameManager; 
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundaries();
        if (gameManager.isGameActive){
            MovePlayer();
            CheckIfCanShoot();
        }
        
    }
    //Move the player by arrow key input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        //https://forum.unity.com/threads/how-to-make-player-object-slightly-tilt-as-it-moves-left-and-right.121725/
        float x = Input.GetAxis("Vertical") * 15.0f; 
        Vector3 euler = transform.localEulerAngles;
        euler.x = Mathf.Lerp(2.0f * Time.deltaTime, x, euler.x);
        transform.localEulerAngles = euler;

    }
    //Prevent the player from leaving from all sides of the screen
    void CheckBoundaries()
    {
        if (transform.position.x > bound){
            transform.position = new Vector3(bound, transform.position.y, transform.position.z);
        }
        
        if (transform.position.x < -bound){
            transform.position = new Vector3(-bound, transform.position.y, transform.position.z);
        }  
    }

    void CheckIfCanShoot(){
        if (Input.GetKeyDown(KeyCode.G) && canShoot){
                //https://answers.unity.com/questions/746960/instantiate-object-in-front-of-player.html
                Instantiate(rocket, playerRb.position+(transform.right*2), transform.rotation);

                canShoot = false;
                StartCoroutine(RocketCooldown());
                Debug.Log("Function");
            }
    }
    
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy")){
            Instantiate(explosionEffect, playerRb.position, transform.rotation);
            Destroy(collision.gameObject);
            gameManager.UpdateLives(-1);
            Debug.Log("Player has collided with an enemy");
        }
    }

    IEnumerator RocketCooldown(){
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
        Debug.Log("Thing");
    }

    
}
