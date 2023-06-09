using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public int health = 3;
    public int points = 5;
    public int speedMultiplier;

    private Rigidbody objectRb;

    private GameManager gameManager;

    public GameObject explosionEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update(){
        UpdateSpeed();
        transform.position += transform.right * speed * Time.deltaTime * speedMultiplier;

        if(transform.position.x <= -40){
            Destroy(gameObject);
            if (gameManager.isGameActive){
                gameManager.UpdateScore(-points*2);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Rocket")){
            health--;
            Destroy(other.gameObject);
            if (health <= 0){
                if (gameManager.isGameActive){
                    gameManager.UpdateScore(points);
                }
                Instantiate(explosionEffect, objectRb.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void UpdateSpeed(){
        speedMultiplier = 1+ gameManager.score/250;
        if (speedMultiplier <= 0){
            speedMultiplier = 0;
        }
    }

}

