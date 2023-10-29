using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState { Idle, Walking, Dashing}

public class PlayerStateManager : MonoBehaviour
{
    // Audio
    public SoundManager sm;

    // Misc.
    public PlayerHealth h;
    public Rigidbody2D playerRigidbody;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capCollider;
    private CharacterGround playerGround;
    public SpriteRenderer sr;
    public PlayerState currentState;
    private PlayerState previousState;
    public float stateLifeTimeTotal = 0f;
    public float stateLifeTimeCurrent = 0f;
    public bool noTimer = false;
    Vector2 dashStart = Vector2.zero;
    Vector2 dashEnd = Vector2.zero;
    //public Vector2 lastGrounded;
    //public bool cm.onGround = false;

    // Attack

    // Jump
    public float jumpPower = 400f;
    private float defaultGravity = 1f;
    public bool jumping = false;
    public bool wasJumping = false;

    // Walking
    private Vector3 movementPosition;
    public float movementSpeed = 30;
    public Vector2 playerInputDirection = Vector2.right;
    public Vector2 playerRBDirection = Vector2.right;
    public float horizontalAxisRaw = 0f;
    public float verticalAxisRaw = 0f;
    public float playerSpeed = 1f;
    public float maxVelocity = 1f;
    public float timeStandingStill = 1f;
    public float continueWalkTime = 0.15f;

    // Dash
    //private float dashSpeed = 25f;
    public float dashDistance = 6f;
    CharacterMovement cm;
    //public Vector3 dashEnd;
    private float dashCooldown = 0.5f;
    private float dashTime = 0.08f;
    private bool canDash = true;
    public float isKnockedBack = 0f;

    // Animation
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sm = FindObjectOfType<SoundManager>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        cm = GetComponent<CharacterMovement>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        h = gameObject.GetComponent<PlayerHealth>();
        playerGround = GetComponent<CharacterGround>();
        sr = GetComponent<SpriteRenderer>();
        ChangeStateTo(PlayerState.Idle);
        canDash = true;
        //lastGrounded = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.identity;

        ExecuteState();

        HandleInput();

        DetermineDirection();

        //GroundedCheck(null);

        if (dashTime <= 0)// && playerGround.GetOnGround())
        {
            if (playerGround.GetOnGround())
            {
                canDash = true;
            }
        }
        else if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }

        if (isKnockedBack > 0f)
        {
            isKnockedBack -= Time.deltaTime;
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
        //horizontalAxisRaw = Input.GetAxisRaw("Horizontal");
        verticalAxisRaw = Input.GetAxisRaw("Vertical");
        //
        //if (horizontalAxisRaw != 0)
        //{
        //    if (currentState != PlayerState.Dashing)
        //    {
        //        ChangeStateTo(PlayerState.Walking);
        //    }
        //}
        //else if (playerRigidbody.velocity.x == 0f && playerRigidbody.velocity.y == 0 && cm.onGround)
        //{
        //    ChangeStateTo(PlayerState.Idle);
        //}
        //
        //if (Input.GetButton("Jump"))
        //{
        //    if (cm.onGround && !jumping)
        //    {
        //        Debug.Log("Jumped");
        //        cm.onGround = false;
        //        //Debug.Log("jumping");
        //        jumping = true;
        //        playerRigidbody.AddForce(transform.up * jumpPower);
        //    }
        //}

        if (Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.LeftShift))
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

    private void DetermineDirection()
    {
        if (horizontalAxisRaw < 0f)
        {
            playerInputDirection = Vector2.left;
        }
        else if (horizontalAxisRaw > 0f)
        {
            playerInputDirection = Vector2.right;
        }

        if (verticalAxisRaw < 0f && !cm.onGround)
        {
            playerInputDirection = Vector2.down;
            animator.SetBool("facingDown", true);
        }
        else
        {
            playerInputDirection = new Vector2(cm.directionX, 0f);
            animator.SetBool("facingDown", false);
        }
    }

    /*
    private void GroundedCheck(Collision2D collision)
    {
        wasJumping = jumping && !cm.onGround;

        if (collision != null)
        {
            bool onMovingPlatform = collision.gameObject.GetComponent<MovingPlatform>() != null;
            cm.onGround = false;
            bool groundSafe = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, 0);
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y > 0.5f)
                {
                    cm.onGround = true;
                }
                else if (hit && hit.collider.GetComponent<Enemy>() != null && hit.collider.GetComponent<MovingPlatform>() != null)
                {
                    groundSafe = false;
                }
            }

            if (hit)
            {
                Debug.Log(hit.collider.gameObject);
            }

            if (!cm.onGround || onMovingPlatform)
            {
                groundSafe = false;
            }

            //cm.onGround = collision.contacts[0].normal.y > 0.5f;
            if (groundSafe)
            {
                lastGrounded = transform.position;
            }
        }
        else
        {
            cm.onGround = Mathf.Approximately(playerRigidbody.velocity.y, 0f);
        }

        if (wasJumping && cm.onGround)
        {
            jumping = false;
        }
        else if (wasJumping)
        {
            jumping = true;
        }
        isKnockedBack = cm.onGround;
        //if (cm.onGround)
        //{
        //    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
        //}
    }
    */
    public void GoToLastGrounded()
    {
        transform.position = playerGround.lastGrounded;
        playerRigidbody.velocity = Vector2.zero;
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
                animator.SetBool("playDash", false);
                playerRigidbody.gravityScale = defaultGravity;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0.01f);
                GetComponent<Collider2D>().isTrigger = false;
                break;
            default:
                break;
        }

        // Enter
        switch (_newState)
        {
            case PlayerState.Idle:
                noTimer = true;
                UseCapsuleCollider(false);
                break;
            case PlayerState.Walking:
                UseCapsuleCollider(false);
                noTimer = true;
                break;
            case PlayerState.Dashing:
                UseCapsuleCollider(true);
                animator.SetBool("playDash", true);
                //stateLifeTimeTotal = dashCooldown / 2f;
                stateLifeTimeTotal = dashTime;
                h.isInvincible = stateLifeTimeTotal;

                //GetComponent<Collider2D>().isTrigger = true;
                playerRigidbody.gravityScale = 0f;
                //playerRigidbody.velocity.Set(playerRigidbody.velocity.x, 0f); // DONT USE velocity.Set() since it messes up the rb velocity permanently!
                //playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0.1f);
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                //Vector2 force = new Vector2(cm.lastXDirection * dashSpeed, 0f); // NOTE: Use this if the RBDirection does not work...
                //force.y = 0f;
                dashStart = transform.position;
                dashEnd = new Vector2(transform.position.x + (cm.lastXDirection * dashDistance), transform.position.y);

                RaycastHit2D hit = Physics2D.Raycast(dashStart, new Vector2(cm.lastXDirection, 0f), Vector2.Distance(dashStart, dashEnd));
                if (hit.collider != null)
                {
                    dashEnd = hit.point;
                    dashEnd.x -= cm.lastXDirection * 0.5f;
                }
                //Debug.Log(force);
                //playerRigidbody.velocity = force;
                //playerRigidbody.AddForce(force);
                //if (cm.onGround)
                //{
                //    cm.desiredVelocity = force;
                //    playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
                //    playerRigidbody.AddForce(force, ForceMode2D.Impulse);
                //}
                //else
                {
                    /*
                    playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
                    playerRigidbody.AddForce(force, ForceMode2D.Impulse);*/
                }

                break;
            default:
                break;
        }

        currentState = _newState;
        stateLifeTimeCurrent = stateLifeTimeTotal;
    }

    private void ExecuteState()
    {
        float stateLifeTimeProgress = stateLifeTimeCurrent / stateLifeTimeTotal;

        switch (currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Walking:
                //if (horizontalAxisRaw != 0f)
                //{
                //    playerRBDirection.x = playerRigidbody.velocity.normalized.x;
                //    playerRBDirection.y = playerRigidbody.velocity.normalized.y;
                //    playerRBDirection.Normalize();
                //
                //    if (verticalAxisRaw >= 0f)
                //    {
                //        if (horizontalAxisRaw < 0f)
                //        {
                //            //playerInputDirection = Vector2.left;
                //
                //            if (playerRigidbody.velocity.x > -maxVelocity || playerRBDirection.x > 0f)
                //            {
                //                playerRigidbody.velocity = new Vector2(playerInputDirection.x * playerSpeed, 0f);
                //                //playerRigidbody.AddForce(new Vector2(playerInputDirection.x * playerSpeed, 0f));
                //            }
                //        }
                //        else
                //        {
                //            //playerInputDirection = Vector2.right;
                //
                //            if (playerRigidbody.velocity.x < maxVelocity || playerRBDirection.x < 0f)
                //            {
                //                playerRigidbody.velocity = new Vector2(playerInputDirection.x * playerSpeed, 0f);
                //                //playerRigidbody.AddForce(new Vector2(playerInputDirection.x * playerSpeed, 0f));
                //            }
                //        }
                //    }
                //}
                break;
            case PlayerState.Dashing:
                transform.position = new Vector2(Mathf.Lerp(dashEnd.x, dashStart.x, InQuad(stateLifeTimeProgress)), dashStart.y);
                break;
        }

        //Vector2 clampValue = new Vector2(Mathf.Clamp(playerRigidbody.velocity.x, -maxVelocity, maxVelocity), playerRigidbody.velocity.y);
        //
        //if (currentState != PlayerState.Dashing)
        //{
        //    playerRigidbody.velocity = clampValue;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            //GroundedCheck(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && !jumping)
        {
            //if (playerRigidbody.velocity.x == 0f)
            //{
            //    foreach (ContactPoint2D contact in collision.contacts)
            //    {
            //
            //        if (contact.normal.x > 0f)
            //        {
            //            canGoLeft = false;
            //        }
            //
            //        if (contact.normal.x < 0f)
            //        {
            //            canGoRight = false;
            //        }
            //    }
            //}
            //GroundedCheck(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            //GroundedCheck(collision);
        }
    }

    private void UseCapsuleCollider(bool _useCapsule)
    {
        boxCollider.enabled = !_useCapsule;
        capCollider.enabled = _useCapsule;
    }

    public float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
    public float OutExpo(float t) => 1 - InExpo(1 - t);
    public float InOutExpo(float t)
    {
        if (t < 0.5) return InExpo(t * 2) / 2;
        return 1 - InExpo((1 - t) * 2) / 2;
    }
    public float InQuart(float t) => t * t * t * t;
    public float OutQuart(float t) => 1 - InQuart(1 - t);
    public float InCubic(float t) => t * t * t;
    public float OutCubic(float t) => 1 - InCubic(1 - t);
    public float InOutCubic(float t)
    {
        if (t < 0.5) return InCubic(t * 2) / 2;
        return 1 - InCubic((1 - t) * 2) / 2;
    }
    public float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
    public float InOutCirc(float t)
    {
        if (t < 0.5) return InCirc(t * 2) / 2;
        return 1 - InCirc((1 - t) * 2) / 2;
    }
    public float InQuad(float t) => t * t;
    public float OutQuad(float t) => 1 - InQuad(1 - t);
    public float InOutQuad(float t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }
}