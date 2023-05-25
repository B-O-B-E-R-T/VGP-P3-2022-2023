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
    private int lives;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)){
            UpdateLives(-1);
        }
        if (Input.GetKeyDown(KeyCode.M)){
            UpdateLives(1);
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd){
        lives += livesToAdd;
        livesText.text = "Lives: " + lives;

    }

    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
    }
}
