using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas userInterfaceCanvas;

    int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = "Best Score: " + bestScore;
        bestScore = 0;
        mainMenuCanvas.enabled = true;
    }

    public void UpdateBestScore(int score)
    {
        if (bestScore <= score)
        {
            bestScore = score;
            bestScoreText.text = "Best Score: " + bestScore;
        }
        else
        {
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        mainMenuCanvas.gameObject.SetActive(false);
        userInterfaceCanvas.gameObject.SetActive(true);
    }
}
