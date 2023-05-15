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

    bool isDead = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        rotationDetection = GetComponent<RotationDetection>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground") && rotationDetection.OnAir && !isDead)
        {
            crashParticleSystem.Play();
            audioSource.Play();
            Debug.Log("Game Over!");
            Invoke("ReloadScene", waitTime);
            isDead = true;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
