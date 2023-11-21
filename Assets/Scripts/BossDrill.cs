using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth p = collision.GetComponent<PlayerHealth>();
        if (p)
        {
            p.TakeDamange(1);
        }
    }
}
