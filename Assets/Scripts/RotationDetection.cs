using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDetection : MonoBehaviour
{
    float totalRotationAmount = 0f;
    float previousRotationAmount = 0f;
    float recentRotationAmount = 0f;
    float recentRotationSign;
    float previousRotationSign;

    bool onAir = false;
    public bool OnAir { get { return onAir; } }

    GameManager gameManager;
    AudioSource audioSource;

    [SerializeField] AudioClip skiSFX;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        recentRotationAmount = transform.rotation.z;

        totalRotationAmount += recentRotationAmount - previousRotationAmount;

        recentRotationSign = Mathf.Sign(totalRotationAmount);
        previousRotationSign = Mathf.Sign(previousRotationAmount);

        if (Mathf.Abs(totalRotationAmount) >= 0.98f && Mathf.Abs(totalRotationAmount) < 1.0f)
        {
            totalRotationAmount = 0f;

            if (onAir && (recentRotationSign == previousRotationSign))
            {
                gameManager.AddToScore();
            }
        }

        previousRotationAmount = recentRotationAmount;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onAir = true;
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onAir = false; 
        }
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(skiSFX);
        }
    }
}
