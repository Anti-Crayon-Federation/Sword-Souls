using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Reached checkpoint");
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.lastCheckpoint = transform.position;
        }
    }
}
