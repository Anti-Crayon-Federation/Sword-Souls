using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbackable : MonoBehaviour
{
    [HideInInspector] public float knockbackForce = 40f;
    private float isKnockedBack = 0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyKnockback(Vector2 _force)
    {
        /*
        Debug.Log(_force.x);
        isKnockedBack = 0.2f;
        Vector2 editedVelocity = rb.velocity;
        if (_force.x != 0f)
        {
            editedVelocity.x = _force.x;
        }

        if (_force.y != 0f)
        {
            editedVelocity.y = _force.y;
        }

        rb.velocity = editedVelocity;
        */
    }
}
