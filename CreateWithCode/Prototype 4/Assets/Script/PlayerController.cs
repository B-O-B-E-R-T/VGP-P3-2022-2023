using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    private float powerupStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup;

    public PowerupType currentPowerup = PowerupType.None;

    public GameObject pelletPrefab;
    private GameObject tmpPellet;
    private Coroutine powerupCountdown;

    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (currentPowerup == PowerupType.Pellets && Input.GetKeyDown(KeyCode.F)){
            LaunchRockets();
        }

        if(currentPowerup == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing){
            smashing = true;
            StartCoroutine(Smash());
        }


    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Powerup")){
            hasPowerup = true;
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerupType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

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

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        //Store the y position before taking off
        floorY = transform.position.y;

        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while(Time.time < jumpTime){
            //move the player up while still keeping their x velocity.
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        //Now move the player down
        while(transform.position.y > floorY){
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        //Cycle through all enemies.
        for (int i = 0; i < enemies.Length; i++){
            //Apply an explosion force that originates from our position.
            if(enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            
        }

        //We are no longer smashing, so set the boolean to false
        smashing = false;
    }


    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Knockback){

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            //Debug.Log("Collided with " + collision.gameObject.name + " with powerup set up " + hasPowerup);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerup.ToString());

        }
    }
    
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpPellet = Instantiate(pelletPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpPellet.GetComponent<PelletBehavior>().Fire(enemy.transform);
        }
    }
}
