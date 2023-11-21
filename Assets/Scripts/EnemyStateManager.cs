using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public enum EnemyState { Spawn, Idle, PatrolArea, GoToPlayer, Attack1, Death, AttackWindup, BossIdle, BossFlyByLeft, BossFlyByRight }
public enum EnemyType { Standard, Bomb, Boss, ChargeKnight, ClamCannon }

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
    PlayerStateManager player;
    Quaternion defaultRotation;
    SoundManager sm;
    float soundTimer;
    float soundTimer2;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sm = FindObjectOfType<SoundManager>();
        transform.rotation = Quaternion.identity;
        currentState = EnemyState.Spawn;
        previousState = EnemyState.Spawn;
        if (type != EnemyType.Boss)
        {
            ChangeState(EnemyState.Idle);
        }
        detectionArea = GetComponentInChildren<DetectionArea>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerStateManager>();
        defaultRotation = transform.rotation;

        if (type == EnemyType.ChargeKnight)
        {
            animator = GetComponentInChildren<Animator>();
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        if (type == EnemyType.ClamCannon)
        {
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        if (sr)
        {
            sr.color = Color.white;
        }
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

        if (sr)
        {
            if (type == EnemyType.ClamCannon)
            {
                sr.flipX = player.transform.position.x < transform.position.x;
            }
            else
            {
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
                if (type == EnemyType.ClamCannon)
                {
                    noTimer = true;
                    transform.rotation = defaultRotation;
                    animator.SetBool("isAttacking", false);
                }

                if (detectionArea && detectionArea.isPlayerInside)
                {
                    ChangeState(EnemyState.Attack1);
                }
                break;
            case EnemyState.PatrolArea:
                if(type==EnemyType.ChargeKnight)
                {
                    if(soundTimer<=0)
                    {
                        sm.PlaySound(sm.soundChargeKnightMoving);
                        soundTimer = sm.soundChargeKnightMoving.clip.length;
                    }
                    if(soundTimer2<=0)
                    {
                        sm.PlaySound(sm.soundChargeKnightNotAttacking);
                        soundTimer2 = sm.soundChargeKnightNotAttacking.clip.length;
                    }
                    
                }
                break;
            case EnemyState.GoToPlayer:

                if (type == EnemyType.ChargeKnight)
                {
                    animator.SetBool("playCharge", false);
                    transform.position = new Vector3(transform.position.x + (5f * Time.deltaTime), transform.position.y);

                    if (soundTimer <= 0)
                    {
                        sm.PlaySound(sm.soundChargeKnightMoving);
                        soundTimer = sm.soundChargeKnightMoving.clip.length;
                    }

                    if (soundTimer2 <= 0)
                    {
                        sm.PlaySound(sm.soundChargeKnightNotAttacking);
                        soundTimer2 = sm.soundChargeKnightNotAttacking.clip.length;
                    }
                }

                if (detectionArea.isPlayerInside)
                {
                    ChangeState(EnemyState.Attack1);
                }
                else
                {
                    ChangeState(EnemyState.PatrolArea);
                }
                break;
            case EnemyState.Attack1:
                if (type == EnemyType.ChargeKnight)
                {
                    animator.SetBool("playCharge", true);
                    if(player.gameObject.transform.position.x < gameObject.transform.position.x)
                    {
                        Quaternion target = Quaternion.Euler(0, 0, 0);
                        transform.rotation = target;
                        transform.position = new Vector3(transform.position.x + (-5f * Time.deltaTime), transform.position.y);
                    }
                    else
                    {
                        Quaternion target = Quaternion.Euler(0, 180, 0);
                        transform.rotation = target;
                        transform.position = new Vector3(transform.position.x + (5f * Time.deltaTime), transform.position.y);
                    }
                    sm.PlaySound(sm.soundChargeKnightDrill);

                    if (soundTimer <= 0)
                    {
                        sm.PlaySound(sm.soundChargeKnightMoving);
                        soundTimer = sm.soundChargeKnightMoving.clip.length;
                    }

                }

                if (type == EnemyType.ClamCannon)
                {
                    noTimer = true;
                    animator.SetBool("isAttacking", true);
                    Vector3 direction = player.transform.position - transform.position;
                    float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                    float offset = 180f;
                    if (sr.flipX)
                    {
                        offset = 0f;
                    }

                    transform.rotation = Quaternion.Euler(0, 0, rotation + offset);

                    if (!detectionArea.isPlayerInside)
                    {
                        ChangeState(EnemyState.Idle);
                    }
                }

                break;
            case EnemyState.Death:
                break;
            case EnemyState.AttackWindup:
                ToggleShake(true);
                break;
            case EnemyState.BossFlyByLeft:
                transform.position = new Vector3(transform.position.x + (-2f * Time.deltaTime), transform.position.y);
                break;
            case EnemyState.BossFlyByRight:
                transform.position = new Vector3(transform.position.x + (2f * Time.deltaTime), transform.position.y);
                break;
        }
    }

    private void ToggleShake(bool _value)
    {
        Shaker s = GetComponentInChildren<Shaker>();

        if (s)
        {
            s.shakeEnabled = _value;
        }
    }

    public virtual void DetermineNextState()
    {
        EnemyState newState = EnemyState.Idle;

        if (type == EnemyType.Boss)
        {
            if (currentState == EnemyState.Spawn)
            {
                //newState = EnemyState.BossIdle;
                newState = EnemyState.BossFlyByLeft;
            }
            else
            {
                if (currentState == EnemyState.BossFlyByLeft)
                {
                    newState = EnemyState.BossFlyByRight;
                }

                if (currentState == EnemyState.BossFlyByRight)
                {
                    newState = EnemyState.BossIdle;
                }
            }
        }
        else
        {
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
        }

        //Debug.Log("Changing state to" + newState);
        ChangeState(newState);
    }

    public void ChangeState(EnemyState _newState)
    {
        noTimer = false;
        stateLifeTimeTotal = 1f; // State lasts 1 second by default
        ToggleShake(false);

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
            case EnemyState.BossFlyByLeft:
                break;
            case EnemyState.BossFlyByRight:
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
            case EnemyState.BossIdle:
                noTimer = true;
                break;
            case EnemyState.BossFlyByLeft:
                stateLifeTimeTotal = 3f;
                break;
            case EnemyState.BossFlyByRight:
                stateLifeTimeTotal = 3f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth e = collision.gameObject.GetComponent<PlayerHealth>();
        if(e != null)
        {
            if (type == EnemyType.ChargeKnight)
            {
                if (currentState == EnemyState.Attack1)
                {
                    e.TakeDamange(1);
                }
            }
            else
            {
                //sker der noget hvis man rammer enemies?
            }
        }
        
    }
}
