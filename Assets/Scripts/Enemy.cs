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
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }

        base.Update();
    }

    public override void DetermineNextState()
    {
        base.DetermineNextState();
    }

    public void TakeDamange(int amount)
    {
        health -= amount;
        takeDamageTimer = 0.1f;

        if (health == 0)
        {
            ChangeState(EnemyState.Death);
        }
    }
}
