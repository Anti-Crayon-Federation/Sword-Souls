using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager p = collision.gameObject.GetComponent<PlayerStateManager>();
        if (p)
        {
            p.h.TakeDamange(1);
            if (p.h.currentHealth > 0)
            {
                p.GoToLastGrounded();
            }

        }
    }
}
