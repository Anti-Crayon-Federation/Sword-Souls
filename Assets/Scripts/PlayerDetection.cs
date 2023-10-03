using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    // This is the tag of your player object
    public string playerTag = "Player";
    public Color highlightColor = Color.yellow; 
    public bool enableHighlight = true; 
    private Color originalColor; 
    private SpriteRenderer spriteRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        // Dont "fuc***g" use this 
        // Docs are A** 
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // This function is called when another object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player has entered the area");
            if (enableHighlight)
            {
                spriteRenderer.color = highlightColor;
            }
        }
    }

    // This function is called when another object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player has left the area");
            if (enableHighlight)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }
}