using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { None, Line, Multiplier, Heal }

public class Powerups : MonoBehaviour
{

    public PowerupType powerupType;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }
}
