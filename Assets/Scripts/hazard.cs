using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard : MonoBehaviour
{
   
    public int damage=1;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        PlayerHealth e = collider.gameObject.GetComponent<PlayerHealth>();
        if (e != null)
        {
            e.TakeDamange(damage);
            Debug.Log(e.currentHealth);
        }
    }
}
