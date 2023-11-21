using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyStateManager
{
    public int health;
    public float takeDamageTimer = 0.1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (takeDamageTimer > 0)
        {
            takeDamageTimer -= Time.deltaTime;
            if (sr)
            {
                sr.color = Color.red;
            }
        }
        else
        {
            if (sr)
            {
                sr.color = Color.white;
            }
        }

        base.Update();
    }

    public override void DetermineNextState()
    {
        base.DetermineNextState();
    }

    public void TakeDamange(int _amount, GameObject _source)
    {
        health -= _amount;
        takeDamageTimer = 0.1f;
        
        if (health <= 0)
        {
            ChangeState(EnemyState.Death);
        }

        Knockbackable k = GetComponent<Knockbackable>();

        if (k != null)
        {
            Vector2 force = (transform.position - _source.transform.position).normalized * k.knockbackForce;
            force.y = 0f;
            k.ApplyKnockback(force);
        }
    }
}
