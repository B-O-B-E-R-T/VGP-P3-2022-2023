using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerupPrefab1; 
    public GameObject powerupPrefab2;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    enum Powerup {powerupPrefab1, powerupPrefab2};
    // Start is called before the first frame update
    void Start()
    {
       

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);
        
    }


    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0) { waveNumber++; SpawnEnemyWave(waveNumber); Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);}
    }

    void SpawnEnemyWave(int enemiesToSpawn){
        for(int i = 0; i < enemiesToSpawn; i++){
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPoint(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPoint(){
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
