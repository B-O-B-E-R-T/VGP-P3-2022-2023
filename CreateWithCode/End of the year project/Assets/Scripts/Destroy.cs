using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float waitTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Countdown(){
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
