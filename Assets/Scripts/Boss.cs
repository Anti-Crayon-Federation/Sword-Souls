using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhase { FirstPhase, SecondPhase };

public class Boss : EnemyStateManager
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void DetermineNextState()
    {
        base.DetermineNextState();
    }
}
