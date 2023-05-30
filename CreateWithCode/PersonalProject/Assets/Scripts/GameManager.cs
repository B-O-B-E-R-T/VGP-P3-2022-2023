using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;

    private float zEnemySpawn = 70.0f;
    private float xSpawnRange = 8.0f;
    private float zPowerupRange = 5.0f;
    private float ySpawn = 0.5f;

    private float powerupSpawnTime = 25.0f;
    private float enemySpawnTime = 5.0f;
    private float startDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void SpawnRandomEnemy(){
        float randomX;
        int randomIndex;
        Vector3 spawnPos;

        for(int i = 0; i < 4; i++){
            randomX = Random.Range(-xSpawnRange, xSpawnRange);
            randomIndex = Random.Range(0, enemies.Length);
            spawnPos = new Vector3(randomX, ySpawn+0.5f, zEnemySpawn);
            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
        }
    }
    
    void SpawnPowerup(){

        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zPowerupRange, zPowerupRange);
        Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);
        int randomIndex = Random.Range(0, powerups.Length);

        Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
    }

}
