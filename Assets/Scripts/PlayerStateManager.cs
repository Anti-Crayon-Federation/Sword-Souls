using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState { Idle, Walking, Dashing}

public class PlayerStateManager : MonoBehaviour
{
    // Misc.
    private Rigidbody2D playerRigidbody;
    public PlayerState currentState;
    private PlayerState previousState;
    public float stateLifeTimeTotal = 0f;
    public float stateLifeTimeCurrent = 0f;
    public bool noTimer = false;

    // Jump
    private float jumpPower = 400f;
    private float defaultGravity = 1f;

    // Walking
    private Vector3 movementPosition;
    public float movementSpeed = 10;
    public Vector2 playerInputDirection = Vector2.right;
    public Vector2 playerRBDirection = Vector2.right;
    private float horizontalAxisRaw = 0f;
    public float playerSpeed = 1f;
    public float maxVelocity = 1f;

    // Dash
    public float dashSpeed = 10f;
    public float dashDistance = 7f;
    //public Vector3 dashEnd;
    public float dashCooldown;
    private float dashTime;
    private bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        ChangeStateTo(PlayerState.Idle);
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteState();

        HandleInput();

        if (dashTime <= 0 && Grounded())
        {
            canDash = true;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        if (stateLifeTimeTotal > 0 && stateLifeTimeCurrent > 0)
        {
            stateLifeTimeCurrent -= Time.deltaTime;
        }
        else if (!noTimer && currentState != previousState)
        {
            ChangeStateTo(previousState);
        }
    }

    private void HandleInput()
    {
        horizontalAxisRaw = Input.GetAxisRaw("Horizontal");

        if (horizontalAxisRaw != 0)
        {
            if (currentState != PlayerState.Dashing)
            {
                ChangeStateTo(PlayerState.Walking);
            }
        }
        else if (playerRigidbody.velocity.x == 0f && playerRigidbody.velocity.y == 0 && Grounded())
        {
            ChangeStateTo(PlayerState.Idle);
        }

        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("attempting to jump");
            if (Grounded() == true)
            {
                //Debug.Log("jumping");
                playerRigidbody.AddForce(transform.up * jumpPower);
            }
        }

        if (Input.GetKey("x"))
        {
            //Debug.Log("Dash attempt");
            if (canDash == true && currentState != PlayerState.Dashing)
            {
                //Debug.Log("dashing!");
                canDash = false;
                dashTime = dashCooldown;
                ChangeStateTo(PlayerState.Dashing);
            }
        }
    }

    private bool Grounded()
    {
        return playerRigidbody.velocity.y == 0f;
    }

    private void ChangeStateTo(PlayerState _newState)
    {
        previousState = currentState;
        noTimer = false;
        stateLifeTimeTotal = 0f;

        // Exit
        switch (previousState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking:
                break;
            case PlayerState.Dashing:
                playerRigidbody.gravityScale = defaultGravity;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            default:
                break;
        }

        // Enter
        switch (_newState)
        {
            case PlayerState.Idle:
                noTimer = true;
                break;
            case PlayerState.Walking:
                noTimer = true;
                break;
            case PlayerState.Dashing:
                playerRigidbody.gravityScale = 0f;
                playerRigidbody.velocity.Set(playerRigidbody.velocity.x, 0f);
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                stateLifeTimeTotal = 0.3f;
                Vector2 force = playerInputDirection * dashSpeed * 10f;
                force.y = 0f;
                //Debug.Log(force);
                playerRigidbody.AddForce(force);
                break;
            default:
                break;
        }

        currentState = _newState;
        stateLifeTimeCurrent = stateLifeTimeTotal;
    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking:
                if (horizontalAxisRaw != 0f)
                {
                    playerRBDirection.x = playerRigidbody.velocity.normalized.x;
                    playerRBDirection.y = playerRigidbody.velocity.normalized.y;
                    playerRBDirection.Normalize();

                    if (horizontalAxisRaw < 0f)
                    {
                        playerInputDirection = Vector2.left;

                        if (playerRigidbody.velocity.x > -maxVelocity || playerRBDirection.x > 0f)
                        {
                            playerRigidbody.AddForce(playerInputDirection * playerSpeed);
                        }
                    }
                    else
                    {
                        playerInputDirection = Vector2.right;

                        if (playerRigidbody.velocity.x < maxVelocity || playerRBDirection.x < 0f)
                        {
                            playerRigidbody.AddForce(playerInputDirection * playerSpeed);
                        }
                    }
                }
                break;
            case PlayerState.Dashing:
                break;
        }

        Vector2 clampValue = new Vector2(Mathf.Clamp(playerRigidbody.velocity.x, -maxVelocity, maxVelocity), playerRigidbody.velocity.y);

        if (currentState != PlayerState.Dashing)
        {
            playerRigidbody.velocity = clampValue;
        }
    }
}