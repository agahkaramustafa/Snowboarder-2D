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
                // Score actions
                Debug.Log("Rotation Detected");
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
}
