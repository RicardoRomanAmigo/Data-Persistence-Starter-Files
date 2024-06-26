using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    [SerializeField] AudioClip myClipPaddle;
    [SerializeField] AudioClip myClipSides;
    float volumePaddle = 0.3f;
    float volumeSides = 1f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;

        if(other.transform.name == "Paddle")
        {
            if(myClipPaddle != null)
            {
                AudioManager.Instance.Volume(volumePaddle);
                AudioManager.Instance.PlaySound(myClipPaddle);
            }
        }
        
        if(other.transform.name == "Cube")
        {
            if(myClipSides != null)
            {
                AudioManager.Instance.Volume(volumeSides);
                AudioManager.Instance.PlaySound(myClipSides);
            }
        }
    }
}
