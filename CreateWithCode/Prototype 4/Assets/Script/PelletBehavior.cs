using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    private float pelletStrength = 15.0f;
    private float aliveTimer = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update(){
        if(homing && target != null){
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }

    }

    public void Fire(Transform newTarget){
        //target = homingTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    void OnCollisionEnter(Collision col){
        if (target != null){
            if (col.gameObject.CompareTag(target.tag)){
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * pelletStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }


}
