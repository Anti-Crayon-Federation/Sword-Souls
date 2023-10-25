using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]

    private Rigidbody2D body;
    private CharacterGround ground;
    private PlayerStateManager playerState;

    [Header("Movement Stats")]
    /*[SerializeField, Range(0f, 20f)][Tooltip("Maximum movement speed")]                                     */ private float maxSpeed = 10f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to reach max speed")]                               */ private float maxAcceleration = 52f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to stop after letting go")]                         */ private float maxDecceleration = 58f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to stop when changing direction")]                  */ private float maxTurnSpeed = 58f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to reach max speed when in mid-air")]               */ private float maxAirAcceleration = 80f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to stop in mid-air when no direction is used")]     */ private float maxAirDeceleration = 80f;
    /*[SerializeField, Range(0f, 100f)][Tooltip("How fast to stop when changing direction when in mid-air")]  */ private float maxAirTurnSpeed = 31f;
    /*[SerializeField][Tooltip("Friction to apply against movement on stick")]                                */ private float friction = 0f;

    [Header("Options")]
    /*[Tooltip("When false, the charcter will skip acceleration and deceleration and instantly move and stop")]*/ public bool useAcceleration = false;

    [Header("Calculations")]
    public float directionX;
    public float lastXDirection;
    public Vector2 desiredVelocity;
    public Vector2 velocity;
    private float maxSpeedChange;
    private float acceleration;
    private float deceleration;
    private float turnSpeed;

    [Header("Current State")]
    public bool onGround;
    public bool pressingKey;

    private void Awake()
    {
        //Find the character's Rigidbody and ground detection script
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<CharacterGround>();
        playerState = GetComponent<PlayerStateManager>();
    }
    /*
    public void OnMovement(InputAction.CallbackContext context)
    {
        //This is called when you input a direction on a valid input type, such as arrow keys or analogue stick
        //The value will read -1 when pressing left, 0 when idle, and 1 when pressing right.

        //if (movementLimiter.instance.CharacterCanMove)
        {
            directionX = context.ReadValue<float>();
        }
    }
    */
    private void Update()
    {
        float newX = Input.GetAxisRaw("Horizontal");

        if (newX > 0)
        {
            newX = Mathf.CeilToInt(newX);
        }
        else if (newX < 0)
        {
            newX = Mathf.FloorToInt(newX);
        }

        if (directionX != 0)
        {
            lastXDirection = directionX;
        }

        if (directionX != newX)
        {
            lastXDirection = directionX;
            directionX = newX;
        }

        //Used to stop movement when the character is playing her death animation
        //if (!movementLimiter.instance.CharacterCanMove)
        {
            //directionX = 0;
        }

        //Used to flip the character's sprite when she changes direction
        //Also tells us that we are currently pressing a direction button
        if (directionX != 0)
        {
            // ????????
            //transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
            bool faceLeft = false;

            if (directionX > 0)
            {
                faceLeft = false;
            }
            else
            {
                faceLeft = true;
            }

            playerState.animator.SetBool("facingLeft", faceLeft);
            playerState.sr.flipX = faceLeft;

            playerState.animator.SetBool("playWalk", true);
            playerState.timeStandingStill = 0f;

            pressingKey = true;
        }
        else
        {
            if (playerState.horizontalAxisRaw == 0f)
            {
                if (playerState.timeStandingStill < 10f)
                {
                    playerState.timeStandingStill += Time.deltaTime;
                }

                if (playerState.timeStandingStill > playerState.continueWalkTime)
                {
                    playerState.animator.SetBool("playWalk", false);
                }
            }
            pressingKey = false;
        }

        playerState.animator.SetFloat("moveSpeed", Mathf.Abs(body.velocity.x));
        playerState.animator.SetFloat("verticalVelocity", body.velocity.y);
        if (body.velocity.y <= 0f)
        {
            playerState.animator.SetBool("playJump", false);
            playerState.animator.SetBool("playFall", true);
        }

        //Calculate's the character's desired velocity - which is the direction you are facing, multiplied by the character's maximum speed
        //Friction is not used in this game
        //if (GetComponent<PlayerStateManager>().currentState != PlayerState.Dashing)
        {
            desiredVelocity = new Vector2(directionX, 0f) * Mathf.Max(maxSpeed - friction, 0f);
        }

    }

    private void FixedUpdate()
    {
        //Fixed update runs in sync with Unity's physics engine

        //Get Kit's current ground status from her ground script
        onGround = ground.GetOnGround();

        //Get the Rigidbody's current velocity
        velocity = body.velocity;

        //Calculate movement, depending on whether "Instant Movement" has been checked
        if (useAcceleration)
        {
            runWithAcceleration();
        }
        else if (playerState.currentState != PlayerState.Dashing)
        {
            if (onGround)
            {
                runWithoutAcceleration();
            }
            else
            {
                runWithAcceleration();
            }
        }
    }

    private void runWithAcceleration()
    {
        //Set our acceleration, deceleration, and turn speed stats, based on whether we're on the ground on in the air

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;

        if (pressingKey)
        {
            //If the sign (i.e. positive or negative) of our input direction doesn't match our movement, it means we're turning around and so should use the turn speed stat.
            if (Mathf.Sign(directionX) != Mathf.Sign(velocity.x))
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                //If they match, it means we're simply running along and so should use the acceleration stat
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            //And if we're not pressing a direction at all, use the deceleration stat
            maxSpeedChange = deceleration * Time.deltaTime;
        }

        //Move our velocity towards the desired velocity, at the rate of the number calculated above
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        //Update the Rigidbody with this new velocity
        body.velocity = velocity;
    }

    private void runWithoutAcceleration()
    {
        //If we're not using acceleration and deceleration, just send our desired velocity (direction * max speed) to the Rigidbody
        velocity.x = desiredVelocity.x;

        body.velocity = velocity;
    }
}

