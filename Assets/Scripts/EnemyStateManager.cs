using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Spawn, Idle, PatrolArea, GoToPlayer, Attack1, Death }

public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Spawn;
    public EnemyState previousState = EnemyState.Spawn;
    public float stateLifeTimeTotal = 0f;
    public float stateLifeTimeCurrent = 0f;
    public bool noTimer = false;
    public bool detectedPlayer = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentState = EnemyState.Spawn;
        previousState = EnemyState.Spawn;
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if ((stateLifeTimeCurrent > 0f && stateLifeTimeTotal > 0f) || noTimer)
        {
            stateLifeTimeCurrent -= Time.deltaTime;
            ExecuteState(currentState);
        }
        else if (stateLifeTimeTotal > 0f)
        {
            stateLifeTimeTotal = 0f;
            DetermineNextState();
        }
    }

    public void ExecuteState(EnemyState _state)
    {
        switch (_state)
        {
            case EnemyState.Spawn:
                Debug.Log("Running Spawn");
                break;
            case EnemyState.Idle:
                Debug.Log("Running Idle");
                if (detectedPlayer)
                {
                    ChangeState(EnemyState.Attack1);
                }
                break;
            case EnemyState.PatrolArea:
                break;
            case EnemyState.GoToPlayer:

                if (detectedPlayer)
                {
                    // Move enemy toward player
                }
                else
                {
                    // Return to Idle or Patrol
                }
                break;
            case EnemyState.Attack1:
                break;
            case EnemyState.Death:
                break;
        }
    }

    public virtual void DetermineNextState()
    {
        if (currentState == EnemyState.Attack1)
        {
            ChangeState(EnemyState.GoToPlayer);
        }
        //ChangeState(previousState);
    }

    public void ChangeState(EnemyState _newState)
    {
        noTimer = false;
        stateLifeTimeTotal = 1f; // State lasts 1 second by default

        if (currentState != _newState)
        {
            previousState = currentState;
        }

        // Exit current state:
        switch (currentState)
        {
            case EnemyState.Spawn:
                Debug.Log("Exit Spawn");
                break;
            case EnemyState.Idle:
                Debug.Log("Exit Idle");
                break;
            case EnemyState.PatrolArea:
                break;
            case EnemyState.GoToPlayer:
                break;
            case EnemyState.Attack1:
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }

        // Enter new state:
        currentState = _newState;

        switch (_newState)
        {
            case EnemyState.Spawn:
                Debug.Log("Enter Spawn");
                break;
            case EnemyState.Idle:
                //noTimer = true;
                stateLifeTimeTotal = 1f;
                Debug.Log("Enter Idle");
                break;
            case EnemyState.PatrolArea:
                break;
            case EnemyState.GoToPlayer:
                break;
            case EnemyState.Attack1:
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }

        stateLifeTimeCurrent = stateLifeTimeTotal;
    }
}
