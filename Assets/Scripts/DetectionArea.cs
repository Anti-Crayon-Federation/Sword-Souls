using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    [SerializeField] private bool enableHighlight = true;
    [SerializeField] private Color highlightColor = Color.yellow;
    private Color originalColor;

    private SpriteRenderer spriteRenderer;
    public bool isPlayerInside = false;
    public Vector2 spottedAtLocation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStateManager player = other.GetComponent<PlayerStateManager>();

        if (player != null)
        {
            Debug.Log("Player has entered the area");
            isPlayerInside = true;
            spottedAtLocation = player.transform.position;
            if (enableHighlight)
            {
                spriteRenderer.color = highlightColor;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerStateManager player = other.GetComponent<PlayerStateManager>();

        if (player != null)
        {
            Debug.Log("Player has left the area");
            isPlayerInside = false;
            spottedAtLocation = transform.position;
            if (enableHighlight)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }
}
