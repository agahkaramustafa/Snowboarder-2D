using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int playerLives = 3;
    int levelScore = 0;
    int totalScore;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeCountText;
    
    void Awake() 
    {
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;

        if (gameManagerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        totalScore = levelScore;

        lifeCountText.text = playerLives.ToString();
        scoreText.text = "360s : " + levelScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            EndGameSession();
        }
    }

    public void AddToScore()
    {
        levelScore += 1;
        scoreText.text = "360s : " + levelScore.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        lifeCountText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void EndGameSession()
    {
        // Enable an end game screen

    }
}
