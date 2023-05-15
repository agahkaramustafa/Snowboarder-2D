using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticleSystem;

    bool isGrounded = true;

    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (!isGrounded && other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Update() 
    {
        if (isGrounded)
        {
            dustParticleSystem.Play();
        }
    }
}
