using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI rulesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject player;
    public bool isGameActive;

    private float xEnemySpawn = 40.0f;
    private float zSpawnRange = 5.0f;
    private float ySpawn = 0f;
    private float enemySpawnRate = 5.0f;
    private float enemyCountRate = 10.0f;
    private int enemyCount = 3;
    private int lives;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRandomEnemy(){
        while (isGameActive){
            yield return new WaitForSeconds(enemySpawnRate);
            float randomZ;
            int randomIndex;
            Vector3 spawnPos;

            for(int i = 0; i < enemyCount; i++){
                randomZ = Random.Range(-zSpawnRange, zSpawnRange);
                randomIndex = Random.Range(0, enemies.Count);
                spawnPos = new Vector3(xEnemySpawn, ySpawn, randomZ);
                Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
            }
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd){
        lives += livesToAdd;
        livesText.text = "Lives: " + lives;
        if (lives <= 0){
            GameOver();
        }
    }

    public void GameOver(){
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        lives = 0;
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(){
        isGameActive = true;
        score = 0;
        lives = 3;
        titleScreen.gameObject.SetActive(false);
        rulesText.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(IncreaseEnemyCount());
    }

    IEnumerator IncreaseEnemyCount(){
        while (isGameActive){
            yield return new WaitForSeconds(enemyCountRate);
            enemyCountRate += 5.0f;
            enemyCount++;            
        }
    }
}
