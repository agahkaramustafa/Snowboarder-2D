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

    bool isOver = false;
    public bool IsOver { get { return isOver; } }

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeCountText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI endScoreText;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas userInterfaceCanvas;
    
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

        userInterfaceCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
    }

    void Start() 
    {
        lifeCountText.text = playerLives.ToString();
        scoreText.text = "360s : " + (totalScore + levelScore).ToString();
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
        scoreText.text = "360s : " + (totalScore + levelScore).ToString();
    }

    void TakeLife()
    {
        StartCoroutine(TakeLifeRoutine());
    }

    IEnumerator TakeLifeRoutine()
    {
        playerLives--;
        lifeCountText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(currentSceneIndex);
        levelScore = 0;
        scoreText.text = "360s : " + (totalScore + levelScore).ToString();
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelRoutine());
    }

    IEnumerator NextLevelRoutine()
    {
        totalScore += levelScore;
        levelScore = 0;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void EndGameSession()
    {

        winText.enabled = false;
        endScoreText.enabled = false;
        gameOverText.enabled = true;
        userInterfaceCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        isOver = true;
    }

    public void WinGame()
    {
        winText.enabled = true;
        endScoreText.text = "Score : " + totalScore.ToString();
        endScoreText.enabled = true;
        gameOverText.enabled = false;
        userInterfaceCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        isOver = true;
    }

    public void PlayAgain()
    {
        levelScore = 0;
        totalScore = 0;
        scoreText.text = "360s : " + (totalScore + levelScore).ToString();
        isOver = false;
        SceneManager.LoadScene(1);
        gameOverCanvas.gameObject.SetActive(false);
        userInterfaceCanvas.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        isOver = false;
    }

    
}
