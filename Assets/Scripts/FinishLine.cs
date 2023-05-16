using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticleSystem;
    [SerializeField] float loadDelay = 2f;

    AudioSource audioSource;
    GameManager gameManager;

    void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            finishParticleSystem.Play();
            audioSource.Play();
            Invoke(nameof(gameManager.NextLevel), loadDelay);
        }
    }

}
