using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public enum EnemyState { Spawn, Idle, PatrolArea, GoToPlayer, Attack1, Death }
public enum EnemyType { Standard, Bomb, Boss }

public class EnemyStateManager : MonoBehaviour
{
    public EnemyType type;
    public EnemyState currentState = EnemyState.Spawn;
    public EnemyState previousState = EnemyState.Spawn;
    public float stateLifeTimeTotal = 0f;
    public float stateLifeTimeCurrent = 0f;
    public bool noTimer = false;
    public DetectionArea detectionArea;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Vector2 facingDirection = Vector2.left;

    // Start is called before the first frame update
    public virtual void Start()
    {
        transform.rotation = Quaternion.identity;
        currentState = EnemyState.Spawn;
        previousState = EnemyState.Spawn;
        ChangeState(EnemyState.Idle);
        detectionArea = GetComponentInChildren<DetectionArea>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 previousPositoin = transform.position;

        if (stateLifeTimeCurrent > 0f && !noTimer)
        {
            stateLifeTimeCurrent -= Time.deltaTime;
        }

        if (stateLifeTimeCurrent > 0f || noTimer)
        {
            ExecuteState(currentState);
        }
        else if (stateLifeTimeCurrent <= 0f)
        {
            stateLifeTimeTotal = 0f;
            DetermineNextState();
        }

        facingDirection = previousPositoin - transform.position;

        if (facingDirection.x >= 0f)
        {
            //animator.SetBool("facingLeft", false);
            sr.flipX = false;
        }
        else
        {
            //animator.SetBool("facingLeft", true);
            sr.flipX = true;
        }
    }

    public void ExecuteState(EnemyState _state)
    {
        switch (_state)
        {
            case EnemyState.Spawn:
                //Debug.Log("Running Spawn");
                break;
            case EnemyState.Idle:
                //Debug.Log("Running Idle");
                if (detectionArea.isPlayerInside)
                {
                    ChangeState(EnemyState.Attack1);
                }
                break;
            case EnemyState.PatrolArea:
                break;
            case EnemyState.GoToPlayer:

                transform.position = new Vector3(transform.position.x + (10f * Time.deltaTime), transform.position.y);

                if (detectionArea.isPlayerInside)
                {
                    // Move enemy toward player
                }
                else
                {
                    // Return to Idle or Patrol
                }
                break;
            case EnemyState.Attack1:
                transform.position = new Vector3(transform.position.x + (-10f * Time.deltaTime), transform.position.y);

                break;
            case EnemyState.Death:
                break;
        }
    }

    public virtual void DetermineNextState()
    {
        EnemyState newState = EnemyState.Idle;

        if (currentState == EnemyState.Idle)
        {
            newState = EnemyState.GoToPlayer;
        }

        if (currentState == EnemyState.Attack1)
        {
            newState = EnemyState.GoToPlayer;
        }

        if (currentState == EnemyState.GoToPlayer)
        {
            newState = EnemyState.Attack1;
        }
        //ChangeState(previousState);

        Debug.Log("Changing state to" + newState);
        ChangeState(newState);
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
                //Debug.Log("Exit Spawn");
                break;
            case EnemyState.Idle:
                //Debug.Log("Exit Idle");
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
                //Debug.Log("Enter Spawn");
                break;
            case EnemyState.Idle:
                //noTimer = true;
                stateLifeTimeTotal = 1f;
                //Debug.Log("Enter Idle");
                break;
            case EnemyState.PatrolArea:
                break;
            case EnemyState.GoToPlayer:
                stateLifeTimeTotal = 1f;
                break;
            case EnemyState.Attack1:
                stateLifeTimeTotal = 1f;
                break;
            case EnemyState.Death:
                if (type == EnemyType.Bomb)
                {
                    SpawnBomb(transform.position, 1f, 0.5f, 1f, 3, true);
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }

        stateLifeTimeCurrent = stateLifeTimeTotal;
    }


    public void SpawnBomb(Vector2 _pos, float _countDown, float _lifeTime, float _size, int _damage, bool _activated)
    {
        GameObject obj = new GameObject("Bomb");
        obj.transform.position = _pos;
        HazardBomb bomb = obj.AddComponent<HazardBomb>();
        bomb.bombCountdown = _countDown;
        bomb.explosionLifetime = _lifeTime;
        bomb.explosionDamage = _damage;
        bomb.explosionSize = _size;
        bomb.bombActivated = _activated;
    }
}
