using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointFunction : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Reached checkpoint");
        PlayerHealth e = collision.GetComponent<PlayerHealth>();
        if (e != null)
        {
            e.lastCheckpoint = transform.position;
        }
    }
}
