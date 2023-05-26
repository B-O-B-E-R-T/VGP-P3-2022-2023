using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public int health = 3;

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
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if(transform.position.x <= -40){
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Rocket")){
            health--;
            Destroy(other.gameObject);
            if (health <= 0){
                gameManager.UpdateScore(5);
                Instantiate(explosionEffect, objectRb.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

}

