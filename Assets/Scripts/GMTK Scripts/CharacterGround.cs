using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGround : MonoBehaviour
{
    private bool onGround;

    [Header("Collider Settings")]
    [SerializeField][Tooltip("Length of the ground-checking collider")] private float groundLength = 0.95f;
    [SerializeField][Tooltip("Distance between the ground-checking colliders")] private Vector3 colliderOffset;

    [Header("Layer Masks")]
    [SerializeField][Tooltip("Which layers are read as the ground")] private LayerMask groundLayer;
    public Vector2 lastGrounded = Vector2.zero;
    private PlayerStateManager playerState;

    private void Start()
    {
        playerState = GetComponent<PlayerStateManager>();
    }

    private void Update()
    {
        //Determine if the player is stood on objects on the ground layer, using a pair of raycasts
        onGround = (Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer)) && playerState.currentState != PlayerState.Dashing;

        if (onGround)
        {
            lastGrounded = transform.position;
            playerState.animator.SetBool("playFall", false);
        }
    }

    private void OnDrawGizmos()
    {
        //Draw the ground colliders on screen for debug purposes
        if (onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    //Send ground detection to other scripts
    public bool GetOnGround() { return onGround; }
}
