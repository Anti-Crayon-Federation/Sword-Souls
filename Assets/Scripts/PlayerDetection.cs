using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    // Player Object
    public string playerTag = "Player";
    // Detection Indicator
    public Color highlightColor = Color.yellow; 
    private Color originalColor; 
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Dont "fuc***g" use this 
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This function is called when another object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player has entered the area");
            spriteRenderer.color = highlightColor;
        }
    }

    // This function is called when another object exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player has left the area");
            spriteRenderer.color = originalColor;
        }
    }
}