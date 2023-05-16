using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticleSystem;

    AudioSource audioSource;
    bool isPassed = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !isPassed)
        {
            isPassed = true;
            finishParticleSystem.Play();
            audioSource.Play();

            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2)
            {
                FindObjectOfType<GameManager>().WinGame();
            }
            else
            {
                FindObjectOfType<GameManager>().NextLevel();
            }

        }
    }

}
