using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetection : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] ParticleSystem crashParticleSystem;

    AudioSource audioSource;
    
    RotationDetection rotationDetection;
    GameManager gameManager;

    bool isDead = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        rotationDetection = GetComponent<RotationDetection>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground") && rotationDetection.OnAir && !isDead)
        {
            crashParticleSystem.Play();
            audioSource.Play();
            Invoke("ReloadScene", waitTime);
            isDead = true;
            gameManager.ProcessPlayerDeath();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
