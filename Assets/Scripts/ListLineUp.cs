using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLineUp : MonoBehaviour
{
    float topBorder = 250.0f;
    float moveSpeed = 1.0f;

    IEnumerator Start()
    {
        while (transform.position.y < topBorder)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            yield return null; // Wait for next frame before checking again
        }

        Destroy(gameObject);
    }
}

