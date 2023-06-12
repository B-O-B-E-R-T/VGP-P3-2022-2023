using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 30.0f;
    private float bound = 18;
    private bool canShoot = true;
    public bool hasPowerup;

    public PowerupType currentPowerup = PowerupType.None;
    private Coroutine powerupCountdown;


    public GameObject rocket;
    public GameObject explosionEffect;
    public GameObject powerupIndicator;
    public GameObject line;
    public GameObject protectionSphere;
    public ParticleSystem explosionParticle;

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

        if (currentPowerup == PowerupType.Protect){
            protectionSphere.gameObject.SetActive(true);
        } else{
            protectionSphere.gameObject.SetActive(false);
        }
        
    }
    //Move the player by arrow key input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        powerupIndicator.transform.position = transform.position;

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
            if (currentPowerup == PowerupType.Multiplier){
                //https://answers.unity.com/questions/1272923/instantiate-with-object-rotation-180-rotation-offs.html
                Instantiate(rocket, playerRb.position+(transform.right*2)+(transform.forward*1), transform.rotation * Quaternion.Euler (0f, 345f, 0f));
                Instantiate(rocket, playerRb.position+(transform.right*2)+(transform.forward*-1), transform.rotation * Quaternion.Euler (0f, 15f, 0f));
            }
            //https://answers.unity.com/questions/746960/instantiate-object-in-front-of-player.html
            Instantiate(rocket, playerRb.position+(transform.right*2), transform.rotation);

            canShoot = false;
            StartCoroutine(RocketCooldown());
        }
    }
    
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy")){
            if (gameManager.isGameActive && currentPowerup != PowerupType.Protect){
                gameManager.UpdateLives(-1);
            }
            Instantiate(explosionEffect, playerRb.position, transform.rotation);
            Destroy(collision.gameObject);
            
            Debug.Log("Player has collided with an enemy");
        }
    }

    IEnumerator RocketCooldown(){
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
        Debug.Log("Thing");
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Powerup")){
            hasPowerup = true;
            currentPowerup = other.gameObject.GetComponent<Powerups>().powerupType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);            
            
            if(powerupCountdown != null)
            {
            StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
            
        }
    }

    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerup = PowerupType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    
}
