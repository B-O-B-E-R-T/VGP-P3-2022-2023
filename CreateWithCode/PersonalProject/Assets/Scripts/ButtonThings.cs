using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonThings : MonoBehaviour
{
    private Button coolButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        coolButton = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        coolButton.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetDifficulty() {
        gameManager.StartGame();
    }
    
}
