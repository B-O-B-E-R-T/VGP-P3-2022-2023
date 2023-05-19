using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject titleScreen;
    public bool isGameActive;
    private int lives;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private float zEnemySpawn = 70.0f;
    private float xSpawnRange = 8.0f;
    private float zPowerupRange = 5.0f;
    private float ySpawn = 0.5f;

    private float powerupSpawnTime = 5.0f;
    private float enemySpawnTime = 5.0f;
    private float startDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnRandomEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRandomEnemy(){
        while(isGameActive){
            yield return new WaitForSeconds(enemySpawnTime);

            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, enemies.Length);
            Vector3 spawnPos = new Vector3(randomX, ySpawn+0.5f, zEnemySpawn);

            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);

            for(int i = 0; i < 3; i++){
                randomX = Random.Range(-xSpawnRange, xSpawnRange);
                randomIndex = Random.Range(0, enemies.Length);
                spawnPos = new Vector3(randomX, ySpawn+0.5f, zEnemySpawn);
                Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
            }
        }
    }
    void SpawnPowerup(){
        while(isGameActive){
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            float randomZ = Random.Range(-zPowerupRange, zPowerupRange);
            Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);
            int randomIndex = Random.Range(0, powerups.Length);

            Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
        }
        
    }
    public void UpdateLives(int livesToChange){
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0){
            lives = 0;
            livesText.text = "Lives: " + lives;
            GameOver();
        }
    }
    public void StartGame(){
        isGameActive = true;

        StartCoroutine(SpawnRandomEnemy());
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }
    public void RestartGame(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver(){
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
}
